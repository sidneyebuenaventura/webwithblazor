namespace DigitalHubUI.Dto.Course;

public class UpdateQuizQuestionRequestDto
{
	public string? Id { get; }
	public string? Title { get; }
	public string Content { get; set; }
	public int Order { get; set; }
	public bool? ToDelete { get; }
	public UpdateQuizQuestionOptionRequestDto[]? QuizQuestionOptions { get; set; } 

	public UpdateQuizQuestionRequestDto(string id, string title, string content, int order,
		UpdateQuizQuestionOptionRequestDto[]? quizQuestionOptions, bool? toDelete)
	{
		Id = id;
		Title = title;
		Content = content;
		Order = order;
		QuizQuestionOptions = quizQuestionOptions;
		ToDelete = toDelete;
	}

	public QuizQuestionDto ToQuizQuestionDto(string parentId)
	{
		return new QuizQuestionDto
		{
			Id = string.IsNullOrEmpty(Id) ? null : Id,
			Title = Content,
			Content = Content,
			Order = Order,
			ParentId = parentId
		};
	}
}

