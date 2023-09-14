using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class QuizAnswerDto : ContentWithParentDto
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.QuizAnswer";
    public string QuizAnswerId { get; set; }
    public QuizQuestionDto[] QuizQuestion { get; set; }
    public QuizQuestionOptionDto[] SelectedOption { get; set; }
    public string UrlName { get; set; }
    public string Owner { get; set; }
}