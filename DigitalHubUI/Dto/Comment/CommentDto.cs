using Newtonsoft.Json;

namespace DigitalHubUI.Dto.Comment;

public class CommentDto
{
	public string Key { get; }
	public string Name { get; }
	public string Message { get; set; }
	public string DateCreated { get; }
	public string ProfilePictureUrl { get; }
	public string ProfilePictureThumbnailUrl { get; }
	public string Status { get; }
	/// <summary>
	/// OriginalContentId + "_" + Language
	/// </summary>
	public string ThreadKey { get; }
	/// <summary>
	/// Serialized JSON string to store extra information, e.g. user profile Id
	/// </summary>
	public string CustomData { get; }
	
	public CommentDto(string key, string name, string message, string dateCreated, string profilePictureUrl, string profilePictureThumbnailUrl, string status, string threadKey, string customData)
	{
		Key = key;
		Name = name;
		Message = message;
		DateCreated = dateCreated;
		ProfilePictureUrl = profilePictureUrl;
		ProfilePictureThumbnailUrl = profilePictureThumbnailUrl;
		Status = status;
		ThreadKey = threadKey;
		CustomData = !string.IsNullOrEmpty(customData) ? customData.Replace("&quot;","\"") : string.Empty;
	}

	public CommentCustomData? CustomDataGetter => string.IsNullOrEmpty(CustomData) ? null : JsonConvert.DeserializeObject<CommentCustomData>(CustomData);
}

public class CommentCustomData
{
	public string? AuthorId { get; }
	
	public CommentCustomData(string? authorId)
	{
		AuthorId = authorId;
	}
}