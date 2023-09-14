using Newtonsoft.Json;

namespace DigitalHubUI.Dto;

public class PublicProfileDto
{
    public class PublicProfileDtoData
    {
        public string Id { get; set; }
        
        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }
        
        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }
        
        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("about", NullValueHandling = NullValueHandling.Ignore)]
        public string About { get; set; }
        
        [JsonProperty("shortbio", NullValueHandling = NullValueHandling.Ignore)]
        public string ShortBio { get; set; }
        [JsonProperty("organisationRole", NullValueHandling = NullValueHandling.Ignore)]
        public string OrganisationRole { get; set; }
        [JsonProperty("organisation", NullValueHandling = NullValueHandling.Ignore)]
        public string Organisation { get; set; }
        [JsonProperty("verificationStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string VerificationStatus { get; set; }
        [JsonProperty("avatarImageId", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarImageId { get; set; }
        [JsonProperty("avatarImageLink", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarImageLink { get; set; }
        [JsonProperty("documentCvId", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentCvId { get; set; }
        [JsonProperty("documentCvUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentCvUrl { get; set; }
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Roles { get; set; }
        [JsonProperty("following", NullValueHandling = NullValueHandling.Ignore)]
        public string Following { get; set; }
        [JsonProperty("lastUserAgreement", NullValueHandling = NullValueHandling.Ignore)]
        public string LastUserAgreement { get; set; }
        [JsonProperty("linkedInUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string LinkedInUrl { get; set; }

    }
    
    // currently, data is returned in field profileRequestDto
    [JsonProperty("profileRequestDto")]
    public PublicProfileDtoData ReturnProfile { get; set; }

    public PublicProfileDto(PublicProfileDtoData returnProfile)
    {
        ReturnProfile = returnProfile;
    }
}

