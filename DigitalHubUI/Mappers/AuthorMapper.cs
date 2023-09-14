using DigitalHubUI.Dto;
using DigitalHubUI.ViewModels;

namespace DigitalHubUI.Mappers;

public static class AuthorMapper
{
    public static AuthorViewModel Map(PublicProfileDto.PublicProfileDtoData profileDto)
    {
        return new AuthorViewModel(string.IsNullOrEmpty(profileDto.Id) ? profileDto.Owner : profileDto.Id, 
            profileDto.FirstName, profileDto.LastName, profileDto.ShortBio, profileDto.AvatarImageLink, profileDto.Organisation, profileDto.OrganisationRole, profileDto.VerificationStatus);
    }
    public static List<AuthorViewModel> Map(List<PublicProfileDto.PublicProfileDtoData> profileDtoList)
    {
        return profileDtoList.Select(profileDto => Map(profileDto)).ToList();
    }
}