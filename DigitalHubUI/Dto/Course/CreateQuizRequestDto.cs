namespace DigitalHubUI.Dto.Course;

public class CreateQuizRequestDto
{
	public string Title { get; set; }
	public int? EstimatedDuration { get; set; }
	public int? PassingRate { get; set; }
	public CreateQuizQuestionRequestDto[]? QuizQuestions { get; set; }


	public CreateQuizRequestDto(string title, int? estimatedDuration, int? passingRate,
		CreateQuizQuestionRequestDto[]? quizQuestions)
	{
		Title = title;
		EstimatedDuration = estimatedDuration;
		PassingRate = passingRate;
		QuizQuestions = quizQuestions;
	}

	public QuizDto ToQuizDto(string parentId)
	{
		return new QuizDto
		{
			Title = Title,
			EstimatedDuration = EstimatedDuration,
			PassingRate = PassingRate,
			ParentId = parentId
		};
	}
}

