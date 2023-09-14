using DigitalHubUI.Utilities;
using Newtonsoft.Json;

namespace DigitalHubUI.Dto.Comment.Api;

public class CustomCommentApiRequestDto
{

	[JsonProperty("commentId")]
	public string CommentId { get; }
	
	[JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
	internal string Message { get; }

	public CustomCommentApiRequestDto(string commentId)
	{
		CommentId = commentId;
	}
	
	public CustomCommentApiRequestDto(string commentId, string message) : this(commentId)
	{
		Message = StringTools.ProcessBeforeSave(message);
	}
}

public class UpdateOrDeleteCommentApiRequestDto
{
	public UpdateOrDeleteCommentApiRequestDto(CustomCommentApiRequestDto commentRequestDto)
	{
		this.commentRequestDto = commentRequestDto;
	}

	[JsonProperty("commentRequestDto")]
	public CustomCommentApiRequestDto commentRequestDto { get; }
}