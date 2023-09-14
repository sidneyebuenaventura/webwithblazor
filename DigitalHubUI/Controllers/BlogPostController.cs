using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using DigitalHubUI.Constants;
using DigitalHubUI.Dto;
using DigitalHubUI.Dto.BlogPost;
using DigitalHubUI.Repositories.Interfaces;
using DigitalHubUI.Services.Interface;
using DigitalHubUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;
using Telerik.Sitefinity.Utilities;
using BlogPostDto = DigitalHubUI.Dto.BlogPostDto;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostController : ControllerWithSitefinitySdk
{
    private readonly ITagRepository _tagRepository;
    private readonly IBlogRepository _blogRepository;
    private readonly IPostRepository _postRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IPostService _postService;
    private readonly IMailService _mailService;

    public BlogPostController(IRestClient restClient, ITagRepository tagRepository, IBlogRepository blogRepository,
        IPostRepository postRepository, IFileRepository fileRepository, IProfileRepository profileRepository,
        IPostService postService, IMailService mailService) : base(restClient)
    {
        _tagRepository = tagRepository;
        _blogRepository = blogRepository;
        _postRepository = postRepository;
        _fileRepository = fileRepository;
        _profileRepository = profileRepository;
        _postService = postService;
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateBlogPostRequestDto model)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to create new article!");
        }

        var currentUser = (string.IsNullOrEmpty(model.BlogPostOwnerId)) ? await _profileRepository.GetCurrentUser() : await _profileRepository.GetUserById(model.BlogPostOwnerId);
        
        IEnumerable<BlogDto> currentBlogs = await _blogRepository.GetBlogsByOwner(currentUser.Owner);
        var currentBlog = (!currentBlogs.Any()) ? null : currentBlogs.First();
        
        if (currentBlog == null)
        {
            var urlName = Regex.Replace(currentUser.FullName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            if (_blogRepository.IsExisted(urlName).Result)
            {
                urlName += "_" + DateTime.Now.ToString(CommonConstants.TimestampFormat);
            }

            var blog = new BlogDto
            {
                UrlName = urlName,
                Title = $"{currentUser.FullName} Blog"
            };
            currentBlog = await _blogRepository.CreateBlog(blog);
            _postService.changeBlogOwner(currentBlog.Id, currentUser.Owner);
        }

        int length = (model.Content.StripHtmlTags().Length < 100) ? model.Content.StripHtmlTags().Length : 100 ;
        string langCheck = model.Content.StripHtmlTags().Substring(0, length);
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://text-analysis12.p.rapidapi.com/language-detection/api/v1.1"),
            Headers = {
                            { "X-RapidAPI-Key", "5592605b9emshbaa9d112f754e7ep148f4djsn7bf7b1cfdc70" },
                            { "X-RapidAPI-Host", "text-analysis12.p.rapidapi.com" },
            },
            Content = new StringContent("{\r\"text\": \"" + langCheck + "\"\r}")
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };
        using (var response2 = await client.SendAsync(request))
        {
            response2.EnsureSuccessStatusCode();
            var body = await response2.Content.ReadAsStringAsync();
            var json = JObject.Parse(body);
            Dictionary<string, string> dictObj = json["language_probability"].ToObject<Dictionary<string, string>>();
            string lang = dictObj.First().Key;
            model.ContentLang = lang;
        }
        model.ParentId = currentBlog.Id;

        // Create new Tags
        var tagsToCreate = model.Tags.Where(x => String.IsNullOrEmpty(x.Id));
        var createdTags = Array.Empty<TagDto>();
        if (tagsToCreate.Any())
        {
            var args = InitRequestArgs();
            createdTags = _tagRepository.CreateTags(tagsToCreate, args).Result.ToArray();
            var createdTagsDict = createdTags.ToDictionary(x => x.Title, x => x.Id);
            var createdBlogPostTags = model.Tags.Select(x =>
                String.IsNullOrEmpty(x.Id) && createdTagsDict.ContainsKey(x.Title)
                    ? new TagDto {Id = createdTagsDict[x.Title], Title = x.Title}
                    : x);
            model.Tags = createdBlogPostTags.ToArray();
        }


        model.UrlName = Regex.Replace(model.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");


        if (_postRepository.IsExisted(model.UrlName).Result)
        {
            model.UrlName += "-" + DateTime.Now.ToString(CommonConstants.TimestampFormat);
        }

        
        // Create new BlogPost
        var blogpost = model.ToBlogPostDto(CurrentUser.Roles.Contains("Administrators") ||
                                           CurrentUser.Roles.Contains("Contributors"));
        blogpost.Owner = currentUser.Owner;
        var createdBlogPost = await _restClient.CreateItem(blogpost);
        
        if (CommonConstants.PublishingStatus.Published.Equals(createdBlogPost.PublishingStatus?.FirstOrDefault()))
        {
            _mailService.SendVerifiedPublicationOfPost(currentUser, createdBlogPost.ItemDefaultUrl);
        } else if (CommonConstants.PublishingStatus.UnderReview.Equals(
                       createdBlogPost.PublishingStatus?.FirstOrDefault()))
        {
            _mailService.SendUnverifiedSubmissionOfPost(currentUser);
            _mailService.SendUnverifiedSubmissionOfPostToAdmin(currentUser, createdBlogPost.ItemDefaultUrl);
        }
       
        _postService.changeBlogPostOwner(createdBlogPost.Id, currentUser.Owner);
        // Relate BlogPost with Image
        // Handle CoverImage upload into Image
        // ##TODO: Assign a proper ParentId (album) for CoverImage
        ImageDto coverImage = null;
        if (model.CoverImageUpload != null)
        {
            var imageInfo = new Dictionary<string, string>
            {
                // Old incorrect parent Id
                // {"ParentId", "73e448de-c66a-40a4-8c79-e37ed16957fb"},
                // Article Cover Images library id
                {"ParentId", "5c9070d8-dae5-4a48-bf92-70be885f277d"},
                {"Title", model.Title},
                {"AlternativeText", model.Title}
            };
            coverImage =
                await _fileRepository.UploadBlogPostCoverImage(imageInfo, model.CoverImageUpload, createdBlogPost);
            createdBlogPost.Owner = currentUser.Owner;
            createdBlogPost = await _restClient.RefreshItem(createdBlogPost);
        }
        // ##TODO: Assign a random image if there is no cover image upload
        // Blocker from Sitefinity nuget defect
        else if (!model.IsDraft)
        {
            coverImage = _fileRepository.GetRandomImagesByCategoryFolder(blogpost.Category[0]).Result;
            await _fileRepository.AddCoverImageToBlogPost(coverImage, createdBlogPost);
            createdBlogPost.Owner = currentUser.Owner;
            createdBlogPost = await _restClient.RefreshItem(createdBlogPost);
        }

        await _postService.EnableTouchPointTracking(createdBlogPost);
        var response = new CreateBlogPostResponseDto(createdBlogPost, createdTags, coverImage);
        return Json(response);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateBlogPostRequestDto model)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to update this article!");
        }
        var blogpost = _restClient.GetItem<BlogPostDto>(model.Id).Result;
        var me = await _profileRepository.GetCurrentUser();
        if (me.Id != blogpost.Owner)
        {
            return Unauthorized("You are not authorized to update this article!");
        }
        
        // Create new Tags
        var tagsToCreate = model.Tags.Where(x => String.IsNullOrEmpty(x.Id));
        var createdTags = Array.Empty<TagDto>();
        if (tagsToCreate.Any())
        {
            var args = InitRequestArgs();
            createdTags = _tagRepository.CreateTags(tagsToCreate, args).Result.ToArray();
            var createdTagsDict = createdTags.ToDictionary(x => x.Title, x => x.Id);
            var createdBlogPostTags = model.Tags.Select(x =>
                String.IsNullOrEmpty(x.Id) && createdTagsDict.ContainsKey(x.Title)
                    ? new TagDto {Id = createdTagsDict[x.Title], Title = x.Title}
                    : x);
            model.Tags = createdBlogPostTags.ToArray();
        }

        // Update BlogPost
        var oldPublishingStatus = blogpost.PublishingStatus?.FirstOrDefault();
        blogpost = model.ToBlogPostDto(blogpost,
            CurrentUser.Roles.Contains("Administrators") || CurrentUser.Roles.Contains("Contributors"));
        await _restClient.UpdateItem(blogpost);
        var newPublishingStatus = blogpost.PublishingStatus?.FirstOrDefault();
        if (CommonConstants.PublishingStatus.Published.Equals(newPublishingStatus) && !CommonConstants.PublishingStatus.Published.Equals(oldPublishingStatus))
        {
            var currentUser = await _profileRepository.GetCurrentUser();
            _mailService.SendVerifiedPublicationOfPost(currentUser, blogpost.ItemDefaultUrl);
        }

        // Handle CoverImage upload into Image
        // Relate BlogPost with Image
        // ##TODO: Assign a proper ParentId (album) for CoverImage
        ImageDto coverImage = null;
        if (model.NewCoverImage)
        {
            if (model.CoverImageUpload != null)
            {
                var imageInfo = new Dictionary<string, string>
                {
                    // Old incorrect parent Id
                    // {"ParentId", "73e448de-c66a-40a4-8c79-e37ed16957fb"},
                    // Article Cover Images library id
                    {"ParentId", "5c9070d8-dae5-4a48-bf92-70be885f277d"},
                    {"Title", model.Title},
                    {"AlternativeText", model.Title}
                };
                coverImage = await _fileRepository.UploadBlogPostCoverImage(imageInfo, model.CoverImageUpload, blogpost);
            }
            else if (!model.IsDraft)
            {
                coverImage = _fileRepository.GetRandomImagesByCategoryFolder(blogpost.Category[0]).Result;
                await _fileRepository.AddCoverImageToBlogPost(coverImage, blogpost);
            }
        }

        var updatedBlogPost = await _restClient.RefreshItem(blogpost);

        var response = new UpdateBlogPostResponseDto(updatedBlogPost, createdTags, coverImage);
        return Json(response);
    }

    [HttpPut("LikeUnlike/{postId}")]
    public async Task<IActionResult> LikeUnlikePost([FromRoute] string postId, [FromQuery(Name = "like")] bool likePost)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to rate this article!");
        }
        var result = await _postService.LikeUnlikePost(postId, likePost);
        return result ? Ok() : BadRequest("Unknown error occured. Please try again!");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery(Name = "id")] string Id)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to delete this article!");
        }
        var blogPostDto = _restClient.GetItem<BlogPostDto>(Id).Result;
        if (!blogPostDto.Owner.Equals(CurrentUser.Id))
        {
            return Unauthorized("You are not authorized to delete this article!");
        }
        
        await _restClient.DeleteItem(blogPostDto);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery(Name = "searchText")] string searchText,
        [FromQuery(Name = "excludedPostIds")] string? excludedPostIdsStr)
    {
        var excludedPostIds = String.IsNullOrEmpty(excludedPostIdsStr) ? new string[] { } : excludedPostIdsStr.Split(",");
        // 1st search returns 24 items, "Show more" returns 12 items
        var count = excludedPostIds.Length == 0 ? 24 : 12;
        List<PostViewModel> posts;

        if (!String.IsNullOrEmpty(searchText) && !String.IsNullOrWhiteSpace(searchText))
        {
            var remainedPostIds = _postRepository.SearchPostIdsByText(searchText, excludedPostIds).Result;
            if (remainedPostIds.Count > 0)
            {
                posts = await _postRepository.GetByIds(remainedPostIds.ToArray(), count);
                if (remainedPostIds.Count - posts.Count > 0)
                {
                    Response.Headers.Add("HasMore", "true");
                }
                return PartialView("~/Views/Shared/DisplayTemplates/PostViewModelList.cshtml", posts);
            }
        }
        
        // search trending posts if there is no posts found with searchText
        Response.Headers.Add("Trending", "true");
        posts = _postRepository.SearchTrendingPosts(12);
        return PartialView("~/Views/Shared/DisplayTemplates/PostViewModelList.cshtml", posts);
    }

    [HttpGet]
    [Route("posts")]
    public async Task<IActionResult> SeeMoreForCategory(
        [FromQuery(Name = "categoryId")] string categoryId,
        [FromQuery(Name = "count")] int count,
        [FromQuery(Name = "excludedPostsIds")] string excludedPostsIds)
    {
        var excludedPostsIdsList =
            excludedPostsIds.Equals("_") ? Array.Empty<string>() : excludedPostsIds.Split(",");
        var remainedPostsIds = _postRepository.GetPostIdsByCategory(categoryId, excludedPostsIdsList);
        var posts = await _postRepository.GetByIds(remainedPostsIds.ToArray(), count);
        if (remainedPostsIds.Count - posts.Count > 0)
        {
            Response.Headers.Add("SeeMore", "true");
        }

        return PartialView("~/Views/Shared/DisplayTemplates/PostViewModelList.cshtml", posts);
    }

    [HttpGet]
    [Route("postsForYou")]
    public async Task<IActionResult> SeeMoreForYou(
        [FromQuery(Name = "count")] int count,
        [FromQuery(Name = "excludedPostsIds")] string excludedPostsIds)
    {
        var excludedPostsIdsList = excludedPostsIds.Equals("_") ? Array.Empty<string>() : excludedPostsIds.Split(",");
        
        ProfileDto profileDto = _profileRepository.GetCurrentUserProfile().Result;
        var remainedPostsIds = _postRepository.GetPostIdsByCategoriesForYou(
            profileDto.Category.Select(c => c.Id).ToArray(), excludedPostsIdsList, count);
        var posts = await _postRepository.GetByIds(remainedPostsIds.ToArray(), count);
        
        if (remainedPostsIds.Count - posts.Count > 0)
        {
            Response.Headers.Add("SeeMore", "true");
        }

        return PartialView("~/Views/Shared/DisplayTemplates/PostViewModelList.cshtml", posts);
    }
}