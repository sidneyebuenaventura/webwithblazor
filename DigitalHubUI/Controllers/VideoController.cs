using DigitalHubUI.Constants;
using DigitalHubUI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerWithSitefinitySdk
{
    private readonly IFileRepository _fileRepository;

	public VideoController(IRestClient restClient, IFileRepository fileRepository) : base(restClient)
	{
		_fileRepository = fileRepository;
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[RequestSizeLimit(CommonConstants.FileSizeLimits.VideoMaxSize)]
	public async Task<IActionResult> Post(IFormFile file)
	{
		if (!CurrentUser.IsAuthenticated)
		{
			return Unauthorized("You must be logged in first to upload new video!");
		}
		
		// ##TODO: Assign proper ParentId, possibly creating new library for the first Video upload
		var videoInfo = new Dictionary<string, string>
		{
			{ "ParentId", CommonConstants.VideosLibrary.DefaultFolder },
			{ "AlternativeText", file.FileName }
		};
		try
		{
			var response = await _fileRepository.UploadVideo(videoInfo, file);
			return Json(response);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return BadRequest(e.Message);
		}

	}
}