using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class QuizQuestionDto : ContentWithParentDto
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.Quizquestion";
    public string Content { get; set; }
    public int Order { get; set; }
    public bool IsArchived { get; set; }
}