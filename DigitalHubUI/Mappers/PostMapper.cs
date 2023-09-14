using DigitalHubUI.Dto;
using DigitalHubUI.Dto.Comment;
using DigitalHubUI.Dto.Comment.Api;
using DigitalHubUI.Utilities;
using DigitalHubUI.ViewModels;
using Progress.Sitefinity.RestSdk.Dto;
using ServiceStack;
using Telerik.Sitefinity.Model.ContentLinks;

namespace DigitalHubUI.Mappers;

public static class PostMapper
{
    public static PostViewModel Map(BlogPostDto blogPostDto, TaxonDto[] categories,
        TaxonDto[] tags, List<PostViewModel> relatedPostsViewModels, PublicProfileDto profileDto, List<CustomCommentDto> commentsDtos = null, List<PublicProfileDto.PublicProfileDtoData> profileDtoList = null)
    {
        var (id,
            title,
            shortTitle,
            summary,
            content,
            owner,
            publicationDate,
            visitsCount,
            likes,
            publishingStatus,
            defaultUrl,
            isFeature,
            coverImageDto,
            commentViewModels,
            certificationLink,
            certificationDto,
            blogPostOwnerId,
            contentLang,
            articleDate) = GetPostDetails(blogPostDto);

        var author = AuthorMapper.Map(profileDto.ReturnProfile);
        var categoryViewModels = categories.Map(CategoryMapper.MapArticleCategory);
        var tagViewModels = tags.Map(TagMapper.Map);
        var additionalAuthors = profileDtoList != null ? AuthorMapper.Map(profileDtoList) : new List<AuthorViewModel>();

        var result = new PostViewModel(id, title, shortTitle, summary, content, publicationDate, owner,
            ImageMapper.Map(coverImageDto), categoryViewModels, tagViewModels,
            commentViewModels, relatedPostsViewModels, visitsCount, likes, publishingStatus, defaultUrl, isFeature, author, certificationLink, ImageMapper.Map(certificationDto, true), blogPostOwnerId, additionalAuthors, contentLang, articleDate);
        if (commentsDtos != null)
        {
            result.Comments = commentsDtos.Select(comment => CommentMapper.Map(comment)).ToList();   
        }
        return result;
    }

    public static PostViewModel Map(BlogPostDto blogPostDto, TaxonDto[] categories,
        TaxonDto[] tags, PublicProfileDto profileDto, List<CustomCommentDto> commentsDtos = null, List<PublicProfileDto.PublicProfileDtoData> profileDtoList = null)
    {
        var (id,
            title,
            shortTitle,
            summary,
            content,
            owner,
            publicationDate,
            visitsCount,
            likes,
            publishingStatus,
            defaultUrl,
            isFeature,
            coverImageDto,
            commentViewModels,
            certificationLink,
            certificationDto,
            blogPostOwnerId,
            contentLang,
            articleDate) = GetPostDetails(blogPostDto);

        var author = AuthorMapper.Map(profileDto.ReturnProfile);
        var categoryViewModels = categories.Map(CategoryMapper.MapArticleCategory);
        var tagViewModels = tags.Map(TagMapper.Map);
        var additionalAuthors = profileDtoList != null ? AuthorMapper.Map(profileDtoList) : new List<AuthorViewModel>();

        var result = new PostViewModel(id, title, shortTitle, summary, content, publicationDate, owner,
            ImageMapper.Map(coverImageDto), categoryViewModels, tagViewModels,
            commentViewModels, visitsCount, likes, publishingStatus, defaultUrl, isFeature, author, certificationLink, ImageMapper.Map(certificationDto, true), blogPostOwnerId, additionalAuthors, contentLang, articleDate);
        if (commentsDtos != null)
        {
            result.Comments = commentsDtos.Select(comment => CommentMapper.Map(comment)).ToList();   
        }
        return result;
    }
    
    private static (string id, string title, string shortTitle, string? summary, string content, string owner, string publicationDate, int visitsCount, string likes, string publishingStatus, string defaultUrl, bool isFeature, ImageDto coverImageDto, List<CommentViewModel> commentViewModels,string certificationLink, ImageDto certificationDto, string blogPostOwnerId, string contentLang, string? articleDate) GetPostDetails(BlogPostDto blogPostDto)
    {
        DateTime formatPublicationDate = DateTime.Parse(blogPostDto.PublicationDate.ToString());
        // var dateArticle = blogPostDto.ArticleDate != null ? blogPostDto.ArticleDate.ToString() : formatPublicationDate.ToString();
        return (
            id: blogPostDto.Id,
            title: blogPostDto.Title,
            shortTitle: blogPostDto.GetValue<string>("ShortTitle"),
            summary: blogPostDto.GetValue<string>("Summary"),
            content: blogPostDto.GetValue<string>("Content"),
            owner: blogPostDto.GetValue<string>("Owner"),
            publicationDate: formatPublicationDate.ToString("dd MMMM yyyy"),
            visitsCount: blogPostDto.VisitsCount ?? 0,
            likes: blogPostDto.Likes,
            publishingStatus: blogPostDto.GetValue<IEnumerable<string>>("PublishingStatus").FirstOrDefault(),
            defaultUrl: blogPostDto.ItemDefaultUrl,
            isFeature: blogPostDto.IsFeatured ?? false,
            coverImageDto: blogPostDto.GetValue<IEnumerable<ImageDto>>("CoverImage").FirstOrDefault(),
            commentViewModels: blogPostDto.GetValue<List<CommentViewModel>>("Comments"),
            certificationLink: blogPostDto.GetValue<string>("CertificationLink"),
            certificationDto: blogPostDto.GetValue<IEnumerable<ImageDto>>("Certification").FirstOrDefault(),
            blogPostOwnerId: blogPostDto.GetValue<string>("Owner"),
            contentLang: blogPostDto.GetValue<string>("ContentLang"),
            articleDate: blogPostDto.GetValue<string>("ArticleDate")
        )!;
    }
}