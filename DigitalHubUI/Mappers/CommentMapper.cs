using DigitalHubUI.Dto.Comment;
using DigitalHubUI.Dto.Comment.Api;
using DigitalHubUI.ViewModels;

namespace DigitalHubUI.Mappers;

public static class CommentMapper
{
	public static CommentViewModel Map(CommentDto comment, ProfileViewModel? author = null)
	{
		var fullName = author?.FullName ?? comment.Name;
		var profilePictureUrl = author?.Avatar ?? comment.ProfilePictureUrl;
		return new CommentViewModel(fullName, comment.Message, comment.DateCreated, profilePictureUrl, comment.Key, comment.CustomDataGetter?.AuthorId, author);
	}

	public static CommentViewModel Map(CustomCommentDto comment, ProfileViewModel? author = null)
	{
		var fullName = author?.FullName ?? comment.Name;
        DateTime formatPublicationDate = DateTime.Parse(comment.AuthorDateTime.ToString());
        return new CommentViewModel(comment.Key, fullName, comment.Message, formatPublicationDate.ToString("dd MMMM yyyy"), author?.Avatar, comment.CustomData?.AuthorId, author);
	}
}
