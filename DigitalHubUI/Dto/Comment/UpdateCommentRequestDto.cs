namespace DigitalHubUI.Dto.Comment;

public class UpdateCommentRequestDto
{
	public string Key { get; }
	public string Message { get; }

	public UpdateCommentRequestDto(string key, string message)
	{
		Key = key;
		Message = message;
	}
}
