namespace DigitalHubUI.Dto.Course;

public class UpdateQuizQuestionOptionRequestDto
{
	public string? Id { get; }
	public string Content { get; set; }
	public bool IsAnswer { get; set; }
	public int Order { get; set; }
	public bool? ToDelete { get; }

	public UpdateQuizQuestionOptionRequestDto(string id, string content, int order, bool isAnswer, bool? toDelete)
	{
		Id = id;
		Content = content;
		Order = order;
		IsAnswer = isAnswer;
		ToDelete = toDelete;
	}

	public QuizQuestionOptionDto ToQuizQuestionOptionDto(string parentId)
	{
		return new QuizQuestionOptionDto
		{
			Id = string.IsNullOrEmpty(Id) ? null : Id,
			Content = Content,
			Order = Order,
			IsAnswer = IsAnswer,
			ParentId = parentId
		};
	}
}

