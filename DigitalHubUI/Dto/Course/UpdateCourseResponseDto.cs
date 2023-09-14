using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Dto.Course;

public class UpdateCourseResponseDto : CreateCourseResponseDto
{
	public UpdateCourseResponseDto(CourseDto course, ImageDto coverImage) : base(course, coverImage)
	{
	}
}