using System.Text.RegularExpressions;
using DigitalHubUI.Constants;
using DigitalHubUI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHubUI.Dto.Course;

[ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "model")]
public class UpdateCourseRequestDto
{
	public string Id { get; }
	public string Title { get; }
	public string? Subtitle { get; }
	public string? Category { get; }
	public string? Level { get; }
	public string? Duration { get; }
	public string? Description { get; }
    public string? AdditionalInfo { get; }
    
    public IFormFile? CoverImageUpload { get; set; }
	public bool ToPublish { get; }
	public bool ToArchive { get; }
	public UpdateModuleRequestDto[]? Modules { get; set; }
	public UpdateQuizRequestDto? Quiz { get; set; }

	public UpdateCourseRequestDto(string id, string title, string? subtitle, string? category, string? level,
		string? duration, string? description, string? additionalInfo, IFormFile? coverImageUpload,
		UpdateModuleRequestDto[]? modules, UpdateQuizRequestDto? quiz, bool toPublish, bool toArchive)
	{
		Id = id;
		Title = title;
		Subtitle = subtitle;
		Category = category;
		Level = level;
		Duration = duration;
		Description = description;
		AdditionalInfo = additionalInfo;
        CoverImageUpload = coverImageUpload;
		Modules = modules;
		Quiz = quiz;
		ToPublish = toPublish;
		ToArchive = toArchive;
	}

	public CourseDto ToCourseDto(CourseDto courseDto)
	{
		return new CourseDto
		{
			Id = Id,
			Title = Title,
			Subtitle = Subtitle,
			coursecategories = Category != null ? new[]{Category} : new string[]{},
			Level = Level,
			Duration = string.IsNullOrEmpty(Duration) ? 0 : int.Parse(Duration),
			Description = Description,
            AdditionalInfo = AdditionalInfo,
			coursepublishingstatuses = new[] { GetCoursepublishingstatus(courseDto.coursepublishingstatuses.First()) },
			UrlName = courseDto.UrlName
		};
	}

	private string GetCoursepublishingstatus(string currentCoursePublishingStatus)
	{
		var coursepublishingstatuses = currentCoursePublishingStatus;
		if (ToPublish)
		{
			coursepublishingstatuses = CommonConstants.CoursePublishingStatus.Published;
		}
		else if (ToArchive)
		{
			coursepublishingstatuses = CommonConstants.CoursePublishingStatus.Archived;
		}

		return coursepublishingstatuses;
	}
}

