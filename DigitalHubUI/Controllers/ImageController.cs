using DigitalHubUI.Constants;
using DigitalHubUI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerWithSitefinitySdk
{
    private readonly IFileRepository _fileRepository;

    public ImageController(IRestClient restClient, IFileRepository fileRepository) : base(restClient)
    {
        _fileRepository = fileRepository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [RequestSizeLimit(CommonConstants.FileSizeLimits.ImageMaxSize)]
    public async Task<IActionResult> Post(IFormFile file, [FromForm] string? folder)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to upload new image!");
        }
        
        // ##TODO: Assign proper ParentId, possibly creating new album for the first Image upload
        var imageInfo = new Dictionary<string, string>();
        switch (folder)
        {
            case "Categories":
            {
                imageInfo.Add("ParentId", CommonConstants.ImagesLibrary.Categories);
                break;
            }
            case "ProfileImages":
            {
                imageInfo.Add("ParentId", CommonConstants.ImagesLibrary.ProfileImages);
                break;
            }
            default:
            {
                imageInfo.Add("ParentId", CommonConstants.ImagesLibrary.DefaultFolder);
                break;
            }
        }

        imageInfo.Add("AlternativeText", file.FileName);

        try
        {
            return Json(await _fileRepository.UploadImage(imageInfo, file));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
    }
}