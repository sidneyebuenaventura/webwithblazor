using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class ModuleCompletionDto : ContentWithParentDto
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.ModuleCompletion";
    public string ModuleCompletionId { get; set; }
    public DateTime CompletionDate { get; set; }
    public ModuleDto[] RelatedModule { get; set; }
}