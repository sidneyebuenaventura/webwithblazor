using DigitalHubUI.ViewModels;

namespace DigitalHubUI.Dto.Course;

public class JoinCourseResponseDto
{
	public string ModuleId { get; }
	public string QuizId { get; }

	public JoinCourseResponseDto(string moduleId, string quizId)
	{
		ModuleId = moduleId;
		QuizId = quizId;
	}
}