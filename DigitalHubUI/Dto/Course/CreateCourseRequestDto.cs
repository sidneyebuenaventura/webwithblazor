using System.Text.RegularExpressions;
using DigitalHubUI.Constants;
using DigitalHubUI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHubUI.Dto.Course;

[ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "model")]
public class CreateCourseRequestDto
{
	public string Title { get; }
	public string? Subtitle { get; }
	public string? Category { get; }
	public string? Level { get; }
	public string? Duration { get; }
	public string? Description { get; }
    public string? AdditionalInfo { get; }
    
    public IFormFile? CoverImageUpload { get; set; }
	public bool ToPublish { get; }
	public CreateModuleRequestDto[]? Modules { get; set; }
	public CreateQuizRequestDto? Quiz { get; set; }

	public CreateCourseRequestDto(string title, string? subtitle, string? category, string? level, string? duration,
		string? description,string? additionalInfo, IFormFile? coverImageUpload, CreateModuleRequestDto[]? modules,
		CreateQuizRequestDto? quiz, bool toPublish)
	{
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
	}

	public CourseDto ToCourseDto()
	{
		return new CourseDto
		{
			Title = Title,
			Subtitle = Subtitle,
			coursecategories = Category != null ? new[]{Category} : new string[]{},
			Level = Level,
			Duration = string.IsNullOrEmpty(Duration) ? 0 : int.Parse(Duration),
			Description = Description,
            AdditionalInfo = AdditionalInfo,
            coursepublishingstatuses = new[] { ToPublish ? CommonConstants.CoursePublishingStatus.Published : CommonConstants.CoursePublishingStatus.Draft},
			UrlName = Regex.Replace(Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-")
		};
	}
}

