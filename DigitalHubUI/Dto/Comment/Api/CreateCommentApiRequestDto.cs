using DigitalHubUI.Utilities;

namespace DigitalHubUI.Dto.Comment.Api;

public class CreateCommentApiRequestDto
{
	public string Name { get; }
	public string Email { get; }
	public string Message { get; }
	public string ProfilePictureUrl { get; }
	public string ProfilePictureThumbnailUrl { get; }
	public string ThreadKey { get; }
	/// <summary>
	/// Serialized JSON string to store extra information, e.g. user profile Id
	/// </summary>
	public string? CustomData { get; }
	public ThreadDto? Thread { get; }
	
	public CreateCommentApiRequestDto(string name, string email, string message, string profilePictureUrl, string profilePictureThumbnailUrl, string threadKey, string? customData, ThreadDto? thread)
	{
		Name = name;
		Email = email;
		Message = StringTools.ProcessBeforeSave(message);
		ProfilePictureUrl = profilePictureUrl;
		ProfilePictureThumbnailUrl = profilePictureThumbnailUrl;
		ThreadKey = threadKey;
		CustomData = customData;
		Thread = thread;
	}
}