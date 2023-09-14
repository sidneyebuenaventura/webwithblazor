namespace DigitalHubUI.Dto.Comment;

public class CreateCommentRequestDto
{
	/// <summary>
	/// Comment message
	/// </summary>
	public string Message { get; }

	/// <summary>
	/// Id of the content to link comments
	/// Used to compute ThreadKey = ContentId + "_" + Language 
	/// At the moment, only BlogPost.Id
	/// </summary>
	public string ContentId { get; }

	/// <summary>
	/// Title of the content commented on
	/// </summary>
	public string Title { get; }

	public CreateCommentRequestDto(string message, string contentId, string title)
	{
		Message = message;
		ContentId = contentId;
		Title = title;
	}
}
