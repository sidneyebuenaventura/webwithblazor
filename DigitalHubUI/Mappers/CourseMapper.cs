using DigitalHubUI.Constants;
using DigitalHubUI.Dto;
using DigitalHubUI.Utilities;
using DigitalHubUI.ViewModels;
using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Mappers;

public static class CourseMapper
{
    public static CourseViewModel Map(CourseDto courseDto, PublicProfileDto authorDto, TaxonDto courseCategory,
        IList<ModuleDto> moduleDtos, QuizDto? quizDto, IList<QuizQuestionDto> quizQuestionDtos,
        IList<QuizQuestionOptionDto> quizQuestionOptionDtos, List<CourseViewModel> relatedCourses)
    {
        var (id, title, subtitle, description, additionalInfo, level, duration, coursePublishingStatus, 
            publicationDate, lastModified, lastModifiedDDMMYYYY, coverImage,
            urlName, itemDefaultUrl, owner) = GetCourseDetails(courseDto);

        return new CourseViewModel(id,
            title,
            subtitle,
            description,
            additionalInfo,
            new LevelViewModel(level, CommonConstants.CourseLevel.GetTitleByNumber(level)),
            duration,
            coursePublishingStatus,
            courseCategory == null ? null : CategoryMapper.MapCourseCategory(courseCategory),
            coverImage,
            publicationDate,
            lastModified,
            lastModifiedDDMMYYYY,
            courseDto.UrlName,
            courseDto.ItemDefaultUrl,
            owner,
            moduleDtos == null ? null : moduleDtos.Select(ModuleMapper.Map).ToList(),
            quizDto == null ? null : QuizMapper.Map(quizDto, quizQuestionDtos, quizQuestionOptionDtos),
            authorDto == null ? null : AuthorMapper.Map(authorDto.ReturnProfile),
            relatedCourses,
            null
        );
    }

    private static (string id, string title, string? subtitle, string? description, string? additionalInfo, string level, int? duration,
        string? coursePublishingStatus, string publicationDate, string lastModified, string lastModifiedDDMMYYYY,
        ImageViewModel? coverImage, string urlName, string itemDefaultUrl, string owner) GetCourseDetails(CourseDto courseDto)
    {
        DateTime formatPublicationDate = DateTime.Parse(courseDto.GetValue<DateTime>("PublicationDate").DayMonthYear("").ToString());
        DateTime lastModifiedDate = DateTime.Parse(courseDto.GetValue<DateTime>("LastModified").ToString());
        return (
            id: courseDto.Id,
            title: courseDto.Title,
            subtitle: courseDto.Subtitle,
            description: courseDto.Description,
            additionalInfo: courseDto.AdditionalInfo,
            level: courseDto.Level,
            duration: courseDto.Duration,
            coursePublishingStatus: courseDto.coursepublishingstatuses is { Length: > 0 }
                ? courseDto.coursepublishingstatuses.First()
                : null,
            publicationDate: formatPublicationDate.ToString("dd MMMM yyyy"),
            lastModified: lastModifiedDate.ToString("dd MMMM yyyy"),
            lastModifiedDDMMYYYY: courseDto.GetValue<DateTime>("LastModified").ToString("d/M/yyyy"),
            coverImage: ImageMapper.Map(courseDto.GetValue<IEnumerable<ImageDto>>("CoverImage").FirstOrDefault()),
            urlName: courseDto.UrlName,
            itemDefaultUrl: courseDto.ItemDefaultUrl,
            owner: courseDto.Owner
        );
    }
}