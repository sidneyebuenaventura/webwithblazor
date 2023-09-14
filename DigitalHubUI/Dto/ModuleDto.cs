using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class ModuleDto : ContentWithParentDto
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.Module";
    public string? Content { get; set; }
    public int Order { get; set; }
    public int? EstimatedDuration { get; set; }
    public string Owner { get; set; }
}