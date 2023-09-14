using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class CourseDto : SdkItem
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.course";
    public string Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Description { get; set; }
    public string? Level { get; set; }
    public int? Duration { get; set; }
    /* Please don't correct field name to camel case, be need it like this to make updating take effect */
    public string[]? coursepublishingstatuses { get; set; }
    public string[]? coursecategories { get; set; }
    public string UrlName { get; set; }
    public string ItemDefaultUrl { get; set; }
    public string Owner { get; set; }
    public int? VisitsCount { get; set; }
    public DateTime? LastInsightSync { get; set; }
    public string? AdditionalInfo { get; set; }
}