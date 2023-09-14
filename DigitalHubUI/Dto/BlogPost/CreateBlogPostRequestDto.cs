using System.Text.RegularExpressions;
using DigitalHubUI.Constants;
using DigitalHubUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using Progress.Sitefinity.RestSdk.Dto;
using Telerik.Sitefinity.Model.ContentLinks;

namespace DigitalHubUI.Dto.BlogPost;

[ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "model")]
public class CreateBlogPostRequestDto
{
	public string Title { get; }
	public string Content { get; }
	public string? ShortTitle { get; }
	public string[]? Category { get; }
	public TagDto[]? Tags { get; set; }
	public IFormFile? CoverImageUpload { get; set; }
	public string? ParentId { get; set; }
	public bool IsDraft { get; }
	public int VisitsCount { get; }
	public string OpenGraphDescription { get; set; }
    public string BlogPostOwnerId { get; set; }
	public string[]? AdditionalAuthors { get; set; }
	public string ContentLang { get; set; }
    public string UrlName { get; set; }

    public CreateBlogPostRequestDto(string title, string content, string shortTitle, string[] category, TagDto[] tags, bool isDraft, string blogPostOwnerId, string[] additionalAuthors, string contentLang, string urlName)
	{
		Title = title;
		Content = content;
		ShortTitle = shortTitle;
		Category = category;
		Tags = tags;
		IsDraft = isDraft;
		VisitsCount = 0;
		OpenGraphDescription = (!string.IsNullOrEmpty(shortTitle)) ? shortTitle : "";
        BlogPostOwnerId = blogPostOwnerId ?? "";
		AdditionalAuthors = additionalAuthors;
		ContentLang = "";
		UrlName = urlName;
    }

	public BlogPostDto ToBlogPostDto(bool createdByContributor = false)
	{
		var blogPost = new BlogPostDto
		{
			Title = Title,
			ParentId = ParentId
		}; 

        blogPost.SetValue("ShortTitle", String.IsNullOrEmpty(ShortTitle) ? "" : ShortTitle);
		blogPost.SetValue("Content", Content);
		blogPost.Category = Category;
		blogPost.Tags = Tags?.Select(x => x.Id).ToArray();

		blogPost.VisitsCount = VisitsCount;
		if (!string.IsNullOrEmpty(OpenGraphDescription))
		{
            blogPost.SetValue("OpenGraphDescription", OpenGraphDescription);
        }

		var PublishingStatus = IsDraft ? CommonConstants.PublishingStatus.Draft :
			(createdByContributor ? CommonConstants.PublishingStatus.Published : CommonConstants.PublishingStatus.UnderReview);
		

		if (AdditionalAuthors != null && AdditionalAuthors.Count() > 0)
        {
			blogPost.AdditionalAuthors = String.Join("|", AdditionalAuthors);

			if (PublishingStatus != CommonConstants.PublishingStatus.Draft)
				PublishingStatus = CommonConstants.PublishingStatus.UnderReview;
		}
        blogPost.SetValue("ContentLang", ContentLang);

        blogPost.SetValue("PublishingStatus", new string[] { PublishingStatus });

		blogPost.UrlName = UrlName;

        return blogPost;
	}
}

