namespace DigitalHubUI.Dto.Course;

public class CreateQuizQuestionRequestDto
{
	public string Title { get; }
	public string Content { get; set; }
	public int Order { get; set; }
	public CreateQuizQuestionOptionRequestDto[]? QuizQuestionOptions { get; set; } 

	public CreateQuizQuestionRequestDto(string title, string content, int order,
		CreateQuizQuestionOptionRequestDto[]? quizQuestionOptions)
	{
		Title = title;
		Content = content;
		Order = order;
		QuizQuestionOptions = quizQuestionOptions;
	}

	public QuizQuestionDto ToQuizQuestionDto(string parentId)
	{
		return new QuizQuestionDto
		{
			Title = Content,
			Content = Content,
			Order = Order,
			ParentId = parentId
		};
	}
}

