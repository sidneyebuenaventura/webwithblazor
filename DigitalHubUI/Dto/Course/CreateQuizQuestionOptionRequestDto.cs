namespace DigitalHubUI.Dto.Course;

public class CreateQuizQuestionOptionRequestDto
{
	public string Content { get; set; }
	public bool IsAnswer { get; set; }
	public int Order { get; set; }

	public CreateQuizQuestionOptionRequestDto(string content, int order, bool isAnswer)
	{
		Content = content;
		Order = order;
		IsAnswer = isAnswer;
	}

	public QuizQuestionOptionDto ToQuizQuestionDto(string parentId)
	{
		return new QuizQuestionOptionDto
		{
			Content = Content,
			Order = Order,
			IsAnswer = IsAnswer,
			ParentId = parentId
		};
	}
}

