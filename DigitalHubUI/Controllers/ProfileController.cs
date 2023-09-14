using System.Net.Mail;
using DigitalHubUI.Constants;
using DigitalHubUI.Dto;
using DigitalHubUI.Dto.Profile;
using DigitalHubUI.Repositories.Interfaces;
using DigitalHubUI.Services.Interface;
using DigitalHubUI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.RestSdk;

namespace DigitalHubUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerWithSitefinitySdk
{
    private readonly IProfileService _profileService;
    private readonly IProfileRepository _profileRepository;
    private readonly IMailService _mailService;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(IRestClient restClient, IProfileService profileService,
        IProfileRepository profileRepository, ILogger<ProfileController> logger, IMailService mailService)
        : base(restClient)
    {
        _profileService = profileService;
        _profileRepository = profileRepository;
        _logger = logger;
        _mailService = mailService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CustomProfileRequestDto model)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to update profile!");
        }
        
        var profile = await _profileRepository.GetCurrentUserProfile();
        
        // We should only update these fields with value from CMS if they are null
        // Until there is no case user can manually remove their uploaded CV)
        if (string.IsNullOrEmpty(model.DocumentCvId))
        {
            model.DocumentCvId = profile.DocumentCvId;
        }
        if (string.IsNullOrEmpty(model.DocumentCvUrl))
        {
            model.DocumentCvUrl = profile.DocumentCvUrl;
        }
        if (string.IsNullOrEmpty(model.LinkedInUrl))
        {
            model.LinkedInUrl = profile.LinkedInUrl;
        }
        if (string.IsNullOrEmpty(model.VerificationStatus))
        {
            model.VerificationStatus = CommonConstants.VerificationStatus.GetTextByNumber(profile.VerificationStatus);
        }
        if (!string.IsNullOrEmpty(model.LastUserAgreement))
        {
            model.LastUserAgreement = profile.LastUserAgreement;
        }

        var response = await _profileService.UpdateProfile(profile.Owner, model);
        return Json(response);
    }

    [Route("verification")]
    [HttpPost]
    public async Task<IActionResult> PostVerification(CustomProfileRequestDto model)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to submit verification!");
        }
        
        var currentUser = await _profileRepository.GetCurrentUser();
        // model doesn't have firstname, lastname, shortBio, about in its context, so we need to migrate it from currentUser
        model.FirstName = string.IsNullOrEmpty(model.FirstName) ? currentUser.FirstName : model.FirstName;
        model.LastName = string.IsNullOrEmpty(model.LastName) ? currentUser.LastName : model.LastName;
        model.ShortBio = string.IsNullOrEmpty(model.ShortBio) ? currentUser.ShortBio : model.ShortBio;
        model.About = string.IsNullOrEmpty(model.About) ? currentUser.About : model.About;
        model.VerificationStatus = (model.DocumentCvId != null) || (model.LinkedInUrl != null)
              ? CommonConstants.VerificationStatus.Pending.Text
            : CommonConstants.VerificationStatus.Unverified.Text;
        
        model.LastUserAgreement = DateTime.Now.ToString();

        var response = await _profileService.UpdateProfile(currentUser.Id, model);

        return Json(response);
    }

    [Route("updatePreferredCategories")]
    [HttpPost]
    public async Task<IActionResult> UpdatePreferredCategories(CustomProfileRequestDto dto)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to update preferred categories!");
        }

        var currentUser = await _profileRepository.GetCurrentUser();
        var publicProfile = _profileService.FindProfileById(currentUser.Id).Result.ReturnProfile;
        CustomProfileRequestDto model = new CustomProfileRequestDto(publicProfile.FirstName, publicProfile.LastName,
            publicProfile.Organisation, publicProfile.ShortBio, publicProfile.About, publicProfile.OrganisationRole,
            publicProfile.AvatarImageId, publicProfile.AvatarImageLink, dto.Categories,
            publicProfile.DocumentCvId, publicProfile.DocumentCvUrl, publicProfile.Following, publicProfile.LastUserAgreement, publicProfile.LinkedInUrl);
        
        model.VerificationStatus = publicProfile.VerificationStatus;
        
        var response = await _profileService.UpdateProfile(currentUser.Id, model);

        return Json(response);
    }
    [Route("updateFollowing")]
    [HttpPost]
    public async Task<IActionResult> UpdateFollowing(CustomProfileRequestDto dto)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to update preferred categories!");
        }

        var currentUser = await _profileRepository.GetCurrentUser();
        List<string> categories = currentUser.PreferredCategories.Select((a) => a.Id).ToList();
        var publicProfile = _profileService.FindProfileById(currentUser.Id).Result.ReturnProfile;
        CustomProfileRequestDto model = new CustomProfileRequestDto(publicProfile.FirstName, publicProfile.LastName,
            publicProfile.Organisation, publicProfile.ShortBio, publicProfile.About, publicProfile.OrganisationRole,
            publicProfile.AvatarImageId, publicProfile.AvatarImageLink, categories,
            publicProfile.DocumentCvId, publicProfile.DocumentCvUrl, dto.Following, publicProfile.LastUserAgreement, publicProfile.LinkedInUrl);

        model.VerificationStatus = publicProfile.VerificationStatus;

        var response = await _profileService.UpdateProfile(currentUser.Id, model);

        return Json(response);
    }
    [Route("updateLastUserAgreement")]
    [HttpPost]
    public async Task<IActionResult> UpdateLastUserAgreement()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Unauthorized("You must be logged in first to update preferred categories!");
        }

        var currentUser = await _profileRepository.GetCurrentUser();
        List<string> categories = currentUser.PreferredCategories.Select((a) => a.Id).ToList();
        var publicProfile = _profileService.FindProfileById(currentUser.Id).Result.ReturnProfile;
        CustomProfileRequestDto model = new CustomProfileRequestDto(publicProfile.FirstName, publicProfile.LastName,
            publicProfile.Organisation, publicProfile.ShortBio, publicProfile.About, publicProfile.OrganisationRole,
            publicProfile.AvatarImageId, publicProfile.AvatarImageLink, categories,
            publicProfile.DocumentCvId, publicProfile.DocumentCvUrl, publicProfile.Following, DateTime.Now.ToString(), publicProfile.LinkedInUrl);

        model.VerificationStatus = publicProfile.VerificationStatus;

        var response = await _profileService.UpdateProfile(currentUser.Id, model);

        return Json(response);
    }

}