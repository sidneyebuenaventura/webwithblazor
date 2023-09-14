using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType("Telerik.Sitefinity.Blogs.Model.BlogPost")]
public class BlogPostDto : ContentWithParentDto
{
    public int? VisitsCount { get; set; }
    
    public DateTime? LastInsightSync { get; set; }
    
    public DateTime? PublicationDate { get; set; }
    
    public string[]? PublishingStatus { get; set; }
    
    public string[]? Tags { get; set; }
    
    public string[]? Category { get; set; }
    
    public bool? IsFeatured { get; set; }
    
    public string Likes { get; set; }

    public string Owner { get; set; }
    public string? AdditionalAuthors { get; set; }
    public DateTime? ArticleDate { get; set; }
}