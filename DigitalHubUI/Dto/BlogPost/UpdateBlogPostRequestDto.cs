using DigitalHubUI.Constants;
using DigitalHubUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Dto.BlogPost;

[ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "model")]
public class UpdateBlogPostRequestDto
{
	public string Id { get; }
	public string Title { get; }
	public string Content { get; }
	public string? ShortTitle { get; }
	public string[]? Category { get; }
	public TagDto[]? Tags { get; set; }
	public IFormFile? CoverImageUpload { get; set; }
	public bool NewCoverImage { get; }
	public bool IsDraft { get; }
    public string OpenGraphDescription { get; }
    

    public UpdateBlogPostRequestDto(string id, string title, string content, string shortTitle, string[] category, TagDto[] tags, bool isDraft, bool newCoverImage)
	{
		Id = id;
		Title = title;
		Content = content;
		ShortTitle = shortTitle;
		Category = category;
		Tags = tags;
		IsDraft = isDraft;
		NewCoverImage = newCoverImage;
        OpenGraphDescription = (!string.IsNullOrEmpty(shortTitle)) ? shortTitle : "";
	}

	public BlogPostDto ToBlogPostDto(BlogPostDto blogPostToUpdate, bool createdByContributor = false)
	{
		blogPostToUpdate.Title = Title;
		blogPostToUpdate.SetValue("ShortTitle", ShortTitle);
		blogPostToUpdate.SetValue("Content", Content);
		blogPostToUpdate.Category = Category;
		blogPostToUpdate.Tags = Tags?.Select(x => x.Id).ToArray();
		var publishingStatus = IsDraft ? CommonConstants.PublishingStatus.Draft :
			(createdByContributor ? CommonConstants.PublishingStatus.Published : CommonConstants.PublishingStatus.UnderReview);
		if (publishingStatus.Equals(CommonConstants.PublishingStatus.Published) &&
            Math.Abs((blogPostToUpdate.PublicationDate-blogPostToUpdate.DateCreated).Value.TotalSeconds) < 2)
		{
			blogPostToUpdate.PublicationDate = DateTime.UtcNow;
        }

        if (!string.IsNullOrEmpty(OpenGraphDescription))
        {
            blogPostToUpdate.SetValue("OpenGraphDescription", OpenGraphDescription); 
        }

        blogPostToUpdate.PublishingStatus = new string[] { publishingStatus };
		return blogPostToUpdate;
	}
}
