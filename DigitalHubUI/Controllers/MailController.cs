using System.Net.Mail;
using DigitalHubUI.Dto.Profile;
using DigitalHubUI.Repositories.Interfaces;
using DigitalHubUI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerWithSitefinitySdk
{
    private readonly IProfileService _profileService;
    private readonly IProfileRepository _profileRepository;
    private readonly IMailService _mailService;
    private readonly ILogger<ProfileController> _logger;

    public MailController(IRestClient restClient, IProfileService profileService,
        IProfileRepository profileRepository, ILogger<ProfileController> logger, IMailService mailService)
        : base(restClient)
    {
        _profileService = profileService;
        _profileRepository = profileRepository;
        _logger = logger;
        _mailService = mailService;
    }

    [Route("sendUnverifiedSignUpMail")]
    [HttpPut]
    public async Task<IActionResult> SendUnverifiedSignUpMail()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to trigger email send!");
        }

        var currentUser = await _profileRepository.GetCurrentUser();

        try
        {
            _mailService.SendUnverifiedSignUpMail(currentUser);
        }
        catch (SmtpException e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.ToString());
        }

        return Ok();
    }

    [Route("sendVerifiedSignUpMail")]
    [HttpPut]
    public async Task<IActionResult> SendVerifiedSignUpMail()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to trigger email send!");
        }
        
        var currentUser = await _profileRepository.GetCurrentUser();

        try
        {
            _mailService.SendVerifiedSignUpMail(currentUser);
        }
        catch (SmtpException e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.ToString());
        }

        return Ok();
    }

    [Route("sendVerificationSubmissionMail")]
    [HttpPut]
    public async Task<IActionResult> SendVerificationSubmissionMail(CustomProfileRequestDto model)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to trigger email send!");
        }

        var currentUser = await _profileRepository.GetCurrentUser();

        try
        {
            _mailService.SendVerificationSubmissionMail(currentUser);
            await _mailService.SendVerificationSubmissionMailToAdmin(currentUser, model.DocumentCvUrl, model.LinkedInUrl);
        }
        catch (SmtpException e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.ToString());
        }

        return Ok();
    }

    [Route("sendPasswordChangedMail")]
    [HttpPut]
    public async Task<IActionResult> SendPasswordChangedMail()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to trigger email send!");
        }
        
        var currentUser = await _profileRepository.GetCurrentUser();

        try
        {
            _mailService.SendPasswordChangedMail(currentUser);
        }
        catch (SmtpException e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.ToString());
        }

        return Ok();
    }
    
}