using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Dto.BlogPost;

public class UpdateBlogPostResponseDto : CreateBlogPostResponseDto
{
	public UpdateBlogPostResponseDto(BlogPostDto blogPost, TagDto[] createdTags, ImageDto coverImage) : base(blogPost, createdTags, coverImage)
	{
	}
}