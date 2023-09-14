using DigitalHubUI.Constants;
using DigitalHubUI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerWithSitefinitySdk
{
    private readonly IFileRepository _fileRepository;

    public DocumentController(IRestClient restClient, IFileRepository fileRepository) : base(restClient)
    {
        _fileRepository = fileRepository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [RequestSizeLimit(CommonConstants.FileSizeLimits.DefaultMaxSize)]
    public async Task<IActionResult> Post(IFormFile file, [FromForm] string? folder)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to upload new document!");
        }

        var documentInfo = new Dictionary<string, string>();
        switch (folder)
        {
            case "presentation":
                documentInfo.Add("ParentId", CommonConstants.DocumentsLibrary.PresentationFolder);
                break;
            case "pdf":
                documentInfo.Add("ParentId", CommonConstants.DocumentsLibrary.PdfFolder);
                break;
            default:
                documentInfo.Add("ParentId", CommonConstants.DocumentsLibrary.DefaultFolder);
                break;
        }

        documentInfo.Add("AlternativeText", file.FileName);
        try
        {
            var response = await _fileRepository.UploadDocument(documentInfo, file);
            return Json(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
    }
}