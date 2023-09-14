using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Dto;

[MappedSitefinityType("Telerik.Sitefinity.Security.Model.SitefinityProfile")]
public class ProfileDto : SdkItem
{
    public string Id { get; set; }
    public string Owner { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string About { get; set; }
    public string ShortBio { get; set; }
    public string OrganisationRole { get; set; }
    public string Organisation { get; set; }
    public string VerificationStatus { get; set; }
    public string DocumentCvId { get; set; }
    public string DocumentCvUrl { get; set; }
    public CategoryDto[] Category { get; set; }
    public string Following { get; set; }
    public string LastUserAgreement { get; set; }
    public string LinkedInUrl { get; set; }

}