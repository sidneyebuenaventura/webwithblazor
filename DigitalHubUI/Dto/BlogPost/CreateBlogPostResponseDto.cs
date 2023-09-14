using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Dto.BlogPost;

public class CreateBlogPostResponseDto
{
	public BlogPostDto BlogPost { get; }
	public TagDto[] CreatedTags { get; }
	public ImageDto CoverImage { get; }
	
	public CreateBlogPostResponseDto(BlogPostDto blogPost, TagDto[] createdTags, ImageDto coverImage)
	{
		BlogPost = blogPost;
		CreatedTags = createdTags;
		CoverImage = coverImage;
	}
}