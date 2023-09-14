using Newtonsoft.Json;

namespace DigitalHubUI.Dto.Comment.Api;

public class CustomCommentDto
{
	
	[JsonProperty("commentId")]
	public string Key { get; set; }
	
	[JsonProperty("authorName")]
	public string Name { get; set; }
	
	[JsonProperty("authorEmail")]
	public string Email { get; set; }

	[JsonProperty("message")]
	public string Message { get; set; }

	[JsonProperty("status")]
	public string Status { get; set; }
	
	[JsonProperty("customData")]
	public CommentCustomData CustomData { get; set; }
	
	[JsonProperty("authorDateTime")]
	public DateTime AuthorDateTime { get; set; }
	
	[JsonProperty("modifyDateTime")]
	public DateTime ModifiedDateTime { get; set; }
	
	public CustomCommentDto(string key, string name, string email, string message, string status, string customData, string authorDateTime, string modifyDateTime)
	{
		Key = key;
		Name = name;
		Email = email;
		Message = message;
		Status = status;
		CustomData = string.IsNullOrEmpty(customData) ? null : JsonConvert.DeserializeObject<CommentCustomData>(customData.Replace("&quot;","\""));
		AuthorDateTime = DateTime.Parse(authorDateTime);
		ModifiedDateTime = DateTime.Parse(modifyDateTime);
	}

}