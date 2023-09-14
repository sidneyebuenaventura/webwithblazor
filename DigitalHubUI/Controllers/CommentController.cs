using DigitalHubUI.Dto.Comment;
using DigitalHubUI.Dto.Comment.Api;
using DigitalHubUI.Mappers;
using DigitalHubUI.Repositories.Interfaces;
using DigitalHubUI.Services.Interface;
using DigitalHubUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerWithSitefinitySdk
{
	private readonly ICommentService _commentService;
	private readonly IProfileRepository _profileRepository;
	
	public CommentController(IRestClient restClient, ICommentService commentService, IProfileRepository profileRepository) : base(restClient)
	{
		_commentService = commentService;
		_profileRepository = profileRepository;
	}
	
	[HttpPost]
    public async Task<IActionResult> Post(CreateCommentRequestDto model)
    {
	    if (!CurrentUser.IsAuthenticated)
	    {
		    return Unauthorized("You must be logged in first to create new comment!");
	    }
	    var me = _profileRepository.GetCurrentUser().Result;
	    var threadKey = model.ContentId + "_en";
	    var fullName = me.FullName;
	    var thread = new ThreadDto(threadKey, model.Title);
	    var customData = JsonConvert.SerializeObject(new CommentCustomData(me.Id));
	    var createCommentRequest =
		    new CreateCommentApiRequestDto(fullName, me.Email, model.Message, me.Avatar, me.Avatar, threadKey, customData, thread);
	    var createCommentResponse = await _commentService.CreateComment(createCommentRequest);
	    var createdComment = CommentMapper.Map(createCommentResponse, me);

	    ViewBag.IsAuthenticated = true;
	    ViewBag.CurrentUser = me;
	    return PartialView("~/Views/Shared/DisplayTemplates/CommentViewModel.cshtml", createdComment);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(UpdateCommentRequestDto model)
    {
	    if (!CurrentUser.IsAuthenticated)
	    {
		    return Unauthorized("You must be logged in first to update this comment!");
	    }
	    var me = await _profileRepository.GetCurrentUser();
	    var comment = await _commentService.QueryComment(model.Key);
	    if (!string.IsNullOrEmpty(comment.CustomData.AuthorId) && me.Id != comment.CustomData.AuthorId)
	    {
		    return Unauthorized("You are not authorized to update this comment!");
	    }
	    var updateCommentRequest = new UpdateOrDeleteCommentApiRequestDto(new CustomCommentApiRequestDto(model.Key, model.Message));
	    await _commentService.UpdateComment(updateCommentRequest);
	    ViewBag.IsAuthenticated = true;
	    ViewBag.CurrentUser = me;
	    comment.Message = model.Message;
	    var updatedComment = CommentMapper.Map(comment, me);
	    updatedComment.Message = StringTools.ProcessBeforeSave(updatedComment.Message);
	    return PartialView("~/Views/Shared/DisplayTemplates/CommentViewModel.cshtml", updatedComment);
    }
    
    [HttpDelete("{commentKey}")]
    public async Task<IActionResult> Delete([FromRoute] string commentKey)
    {
	    if (!CurrentUser.IsAuthenticated)
	    {
		    return Unauthorized("You must be logged in first to delete this comment!");
	    }
	    var me = await _profileRepository.GetCurrentUser();
	    var comment = await _commentService.QueryComment(commentKey);
	    if (!string.IsNullOrEmpty(comment.CustomData?.AuthorId) && me.Id != comment.CustomData.AuthorId)
	    {
		    return Unauthorized("You are not authorized to delete this comment!");
	    }
	    var result = await _commentService.DeleteComment(commentKey);
	    return Ok();
    }
}
