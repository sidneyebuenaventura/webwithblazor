using DigitalHubUI.Constants;
using DigitalHubUI.Dto;
using DigitalHubUI.ViewModels;

namespace DigitalHubUI.Mappers;

public static class CourseEnrollmentMapper
{
    public static CourseEnrollmentViewModel Map(CourseEnrollmentDto moduleDto)
    {
        var (id, 
            certificateUrl, 
            enrolmentStatus, 
            enrolmentDate, 
            completionDate,
            urlName) = GetCourseEnrollmentDetails(moduleDto);

        return new CourseEnrollmentViewModel(id, certificateUrl, enrolmentStatus, enrolmentDate, completionDate, urlName);
    }
    
    private static (string id, 
        string? certificateUrl, 
        string enrolmentStatus, 
        DateTime enrolmentDate, 
        DateTime? completionDate, 
        string urlName
        ) GetCourseEnrollmentDetails(CourseEnrollmentDto courseEnrollmentDto)
    {
        return (
            id: courseEnrollmentDto.Id,
            certificateUrl: courseEnrollmentDto.CertificateUrl,
            enrolmentStatus: courseEnrollmentDto.EnrolmentStatus,
            enrolmentDate: courseEnrollmentDto.EnrolmentDate,
            completionDate: courseEnrollmentDto.CompletionDate,
            urlName: courseEnrollmentDto.UrlName
        );
    }

    /*CourseEnrollmentAnalyticViewModel*/
    public static EnrollmentAnalyticViewModel Map(CourseEnrollmentDto courseEnrollmentDto,
        PublicProfileDto publicProfileDto, QuizAttemptDto? quizAttemptDto)
    {
        return new EnrollmentAnalyticViewModel(
            courseEnrollmentDto.EnrolmentDate.ToString("dd-MM-yyyy"), 
            courseEnrollmentDto.EnrolmentStatus.Equals(CommonConstants.EnrolmentStatus.Completed.Number)
                ? "Completed" : "In progress", 
            AuthorMapper.Map(publicProfileDto.ReturnProfile), 
            quizAttemptDto != null ? QuizAttemptMapper.Map(quizAttemptDto, new List<QuizAnswerDto>(), 
                new List<QuizQuestionDto>(), new List<QuizQuestionOptionDto>()) 
                : null);
    }
    
}