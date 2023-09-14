using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class QuizAttemptDto : ContentWithParentDto
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.QuizAttempt";
    public string QuizAttemptId { get; set; }
    public DateTime AttemptDate { get; set; }
    public int? TotalScore { get; set; }
    public string UrlName { get; set; }
    public string Owner { get; set; }
}