using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class QuizQuestionOptionDto : ContentWithParentDto
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.QuizQuestionOption";
    public string Content { get; set; }
    public bool IsAnswer { get; set; }
    public int Order { get; set; }
    public bool IsArchived { get; set; }
}