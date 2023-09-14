using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType(SitefinityType)]
public class CourseEnrollmentDto : ContentWithParentDto
{
    public const string SitefinityType = "Telerik.Sitefinity.DynamicTypes.Model.LearningHub.CourseEnrolment";
    public string EnrolmentId { get; set; }
    public string? CertificateUrl { get; set; }
    public string EnrolmentStatus { get; set; }
    public DateTime EnrolmentDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string UrlName { get; set; }
    public string Owner { get; set; }
}