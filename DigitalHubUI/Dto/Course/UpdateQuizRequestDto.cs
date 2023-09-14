namespace DigitalHubUI.Dto.Course;

public class UpdateQuizRequestDto
{
	public string? Id { get; }
	public string Title { get; set; }
	public int? EstimatedDuration { get; set; }
	public int? PassingRate { get; set; }
	public UpdateQuizQuestionRequestDto[]? QuizQuestions { get; set; }


	public UpdateQuizRequestDto(string id, string title, int estimatedDuration, int passingRate,
		UpdateQuizQuestionRequestDto[]? quizQuestions)
	{
		Id = id;
		Title = title;
		EstimatedDuration = estimatedDuration;
		PassingRate = passingRate;
		QuizQuestions = quizQuestions;
	}

	public QuizDto ToQuizDto(string parentId)
	{
		return new QuizDto
		{
			Id = string.IsNullOrEmpty(Id) ? null : Id,
			Title = Title,
			EstimatedDuration = EstimatedDuration,
			PassingRate = PassingRate,
			ParentId = parentId
		};
	}
}

