using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Dto.Course;

public class CreateCourseResponseDto
{
	public CourseDto Course { get; }
	public ImageDto CoverImage { get; }
	
	public CreateCourseResponseDto(CourseDto course, ImageDto coverImage)
	{
		Course = course;
		CoverImage = coverImage;
	}
}
