using Newtonsoft.Json;

namespace DigitalHubUI.Dto.Profile;

public class CustomProfileRequestDto
{
    [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
    public string? FirstName { get; set; }

    [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastName { get; set; }

    [JsonProperty("organisation", NullValueHandling = NullValueHandling.Ignore)]
    public string? Organisation { get; set; }

    [JsonProperty("shortBio", NullValueHandling = NullValueHandling.Ignore)]
    public string? ShortBio { get; set; }

    [JsonProperty("about", NullValueHandling = NullValueHandling.Ignore)]
    public string? About { get; set; }

    [JsonProperty("organisationRole", NullValueHandling = NullValueHandling.Ignore)]
    public string? OrganisationRole { get; set; }

    [JsonProperty("verificationStatus", NullValueHandling = NullValueHandling.Ignore)]
    public string? VerificationStatus { get; set; }

    [JsonProperty("avatarImageId", NullValueHandling = NullValueHandling.Ignore)]
    public string? AvatarImageId { get; set; }

    [JsonProperty("avatarImageLink", NullValueHandling = NullValueHandling.Ignore)]
    public string? AvatarImageLink { get; set; }

    [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? Categories { get; set; }
    
    [JsonProperty("documentCvId", NullValueHandling = NullValueHandling.Ignore)]
    public string? DocumentCvId { get; set; }
    
    [JsonProperty("documentCvUrl", NullValueHandling = NullValueHandling.Ignore)]
    public string? DocumentCvUrl { get; set; }

    [JsonProperty("following", NullValueHandling = NullValueHandling.Ignore)]
    public string? Following { get; set; }

    [JsonProperty("lastUserAgreement", NullValueHandling = NullValueHandling.Ignore)]
    public string? LastUserAgreement { get; set; }

    [JsonProperty("linkedInUrl", NullValueHandling = NullValueHandling.Ignore)]
    public string? LinkedInUrl { get; set; }

    public CustomProfileRequestDto(string firstName, string lastName, string? organisation,
        string? shortBio, string? about, string? organisationRole, string? avatarImageId, string? avatarImageLink,
         List<string> categories, string documentCvId, string documentCvUrl, string following, string lastUserAgreement, string linkedInUrl)
    {
        FirstName = firstName;
        LastName = lastName;
        Organisation = organisation;
        ShortBio = shortBio;
        About = about;
        OrganisationRole = organisationRole;
        AvatarImageId = avatarImageId;
        AvatarImageLink = avatarImageLink;
        Categories = categories;
        DocumentCvId = documentCvId;
        DocumentCvUrl = documentCvUrl;
        Following = following;
        LastUserAgreement = lastUserAgreement;
        LinkedInUrl = linkedInUrl;
    }
}

public class UpdateProfileRequestDto
{
    [JsonProperty("postProfileDto")]
    public CustomProfileRequestDto profileRequestDto { get; set; }

    public UpdateProfileRequestDto(CustomProfileRequestDto profileRequestDto)
    {
        this.profileRequestDto = profileRequestDto;
    }
}