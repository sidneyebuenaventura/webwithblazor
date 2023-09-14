using DigitalHubUI.Constants;
using DigitalHubUI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AudioController : ControllerWithSitefinitySdk
{
	private readonly IFileRepository _fileRepository;

	public AudioController(IRestClient restClient, IFileRepository fileRepository) : base(restClient)
	{
		_fileRepository = fileRepository;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[RequestSizeLimit(CommonConstants.FileSizeLimits.AudioMaxSize)]
	public async Task<IActionResult> Post(IFormFile file, [FromForm] string? folder)
	{
		if (!CurrentUser.IsAuthenticated)
		{
			return Unauthorized("You must be logged in first to upload new audio!");
		}

		var documentInfo = new Dictionary<string, string>
		{
			{ "ParentId", CommonConstants.DocumentsLibrary.AudioFolder },
			{ "AlternativeText", file.FileName }
		};
		try
		{
			var response = await _fileRepository.UploadDocument(documentInfo, file);
			response.MimeType = file.ContentType;
			return Json(response);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return BadRequest(e.Message);
		}
	}
}