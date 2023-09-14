using DigitalHubUI.Constants;
using DigitalHubUI.Dto;
using DigitalHubUI.ViewModels;
using DigitalHubUI.ViewModels.Categories;
using Progress.Sitefinity.RestSdk.Clients.Users.Dto;
using ServiceStack;

namespace DigitalHubUI.Mappers;

public static class ProfileMapper
{
    public static ProfileViewModel Map(UserDto userDto, ProfileDto profileDto)
    {
        var (id,
            firstName,
            lastName,
            email,
            about,
            shortBio,
            organisation,
            organisationRole,
            owner,
            preferredCategories,
            following,
            lastUserAgreement) = GetProfileDetails(profileDto);

        userDto.TryGetValue<string>("Avatar", out var avatar);
        userDto.TryGetValue<string>("Email", out email);

        return new ProfileViewModel(id, firstName, lastName, email, avatar, about, shortBio, organisation,
            organisationRole,
            IsVerifiedAccount(userDto, profileDto), IsPendingVerification(userDto, profileDto), owner,
            preferredCategories,
            new List<PostViewModel>(), new List<CourseViewModel>(), following, lastUserAgreement);
    }

    public static ProfileViewModel Map(UserDto userDto, ProfileDto profileDto, List<PostViewModel> posts)
    {
        var (id,
            firstName,
            lastName,
            email,
            about,
            shortBio,
            organisation,
            organisationRole,
            owner,
            preferredCategories,
            following,
            lastUserAgreement) = GetProfileDetails(profileDto);

        userDto.TryGetValue<string>("Avatar", out var avatar);
        userDto.TryGetValue<string>("Email", out email);

        return new ProfileViewModel(id, firstName, lastName, email, avatar, about, shortBio, organisation,
            organisationRole,
            IsVerifiedAccount(userDto, profileDto), IsPendingVerification(userDto, profileDto), owner,
            preferredCategories, posts, new List<CourseViewModel>(), following, lastUserAgreement);
    }

    public static ProfileViewModel Map(PublicProfileDto.PublicProfileDtoData publicProfileDto, ProfileDto profileDto,
        List<PostViewModel> posts)
    {
        var (id,
            firstName,
            lastName,
            email, 
            about,            
            shortBio,
            organisation,
            organisationRole,
            owner,
            preferredCategories,
            following,
            lastUserAgreement) = GetProfileDetails(profileDto);

        var avatar = publicProfileDto.AvatarImageLink;
        shortBio = publicProfileDto.ShortBio;

        return new ProfileViewModel(id, firstName, lastName, email, avatar, about, shortBio, organisation,
            organisationRole,
            IsVerifiedAccount(publicProfileDto), IsPendingVerification(publicProfileDto, profileDto.DocumentCvId, profileDto.LinkedInUrl), owner, preferredCategories,
            posts, new List<CourseViewModel>(), following, lastUserAgreement);
    }

    public static ProfileViewModel Map(PublicProfileDto.PublicProfileDtoData publicProfileDto, ProfileDto profileDto,
        List<CourseViewModel> courses)
    {
        var (id,
            firstName,
            lastName,
            email,
            about,
            shortBio,
            organisation,
            organisationRole,
            owner,
            preferredCategories,
            following,
            lastUserAgreement) = GetProfileDetails(profileDto);

        var avatar = publicProfileDto.AvatarImageLink;
        shortBio = publicProfileDto.ShortBio;

        return new ProfileViewModel(id, firstName, lastName, email, avatar, about, shortBio, organisation,
            organisationRole,
            IsVerifiedAccount(publicProfileDto), IsPendingVerification(publicProfileDto, profileDto.DocumentCvId, profileDto.LinkedInUrl), owner, preferredCategories,
            new List<PostViewModel>(), courses, following, lastUserAgreement);
    }

    public static ProfileViewModel Map(PublicProfileDto.PublicProfileDtoData publicProfileDto, ProfileDto profileDto,
        List<PostViewModel> posts, List<CourseViewModel> courses)
    {
        var (id,
            firstName,
            lastName,
            email,
            about,
            shortBio,
            organisation,
            organisationRole,
            owner,
            preferredCategories,
            following,
            lastUserAgreement) = GetProfileDetails(profileDto);

        var avatar = publicProfileDto.AvatarImageLink;
        shortBio = publicProfileDto.ShortBio;
        email = publicProfileDto.Email;
        following = publicProfileDto.Following;
        lastUserAgreement = publicProfileDto.LastUserAgreement;
        return new ProfileViewModel(id, firstName, lastName, email, avatar, about, shortBio, organisation,
            organisationRole,
            IsVerifiedAccount(publicProfileDto), IsPendingVerification(publicProfileDto, profileDto.DocumentCvId, profileDto.LinkedInUrl), owner, preferredCategories,
            posts, courses, following, lastUserAgreement);
    }
    
    public static ProfileViewModel Map(PublicProfileDto.PublicProfileDtoData publicProfileDto, ProfileDto profileDto,
        int postsCount, int coursesCount)
    {
        var (id,
            firstName,
            lastName,
            email,
            about,
            shortBio,
            organisation,
            organisationRole,
            owner,
            preferredCategories,
            following,
            lastUserAgreement) = GetProfileDetails(profileDto);

        var avatar = publicProfileDto.AvatarImageLink;
        shortBio = publicProfileDto.ShortBio;
        email = publicProfileDto.Email;
        following = publicProfileDto.Following;
        lastUserAgreement = publicProfileDto.LastUserAgreement;

        return new ProfileViewModel(id, firstName, lastName, email, avatar, about, shortBio, organisation,
            organisationRole,
            IsVerifiedAccount(publicProfileDto), IsPendingVerification(publicProfileDto, profileDto.DocumentCvId, profileDto.LinkedInUrl), owner, preferredCategories,
            postsCount, coursesCount, following, lastUserAgreement);
    }

    private static (string id, string firstName, string lastName,string email, string about, string shortBio,
        string
        organisationRole, string designation, string owner, List<ArticleCategoryViewModel> preferredCategories, string following, string lastUserAgreement)
        GetProfileDetails(
            ProfileDto profileDto)
    {
        return (
            id: profileDto.Owner,
            firstName: profileDto.FirstName,
            lastName: profileDto.LastName,
            email: profileDto.Email,
            about: profileDto.About,
            shortBio: profileDto.ShortBio,
            organisation: profileDto.Organisation,
            organisationRole: profileDto.OrganisationRole,
            owner: profileDto.Owner,
            preferredCategories: profileDto.Category.IsEmpty()
                ? new List<ArticleCategoryViewModel>()
                : profileDto.Category.Select(CategoryMapper.MapArticleCategory).ToList()!,
            following: profileDto.Following,
            lastUserAgreement : profileDto.LastUserAgreement
        );
    }

    // status Verified and has role Contributor
    private static bool IsVerifiedAccount(UserDto userDto, ProfileDto profileDto)
        => userDto.Roles != null && userDto.Roles.Any(r => r.Equals(CommonConstants.ContributorsRoleName))
                                 && CommonConstants.VerificationStatus.Verified.Number.Equals(profileDto
                                     .VerificationStatus);

    // status Verified or Pending or has role Contributor and has uploaded CV
    private static bool IsPendingVerification(UserDto userDto, ProfileDto profileDto)
        => !IsVerifiedAccount(userDto, profileDto)
           && (CommonConstants.VerificationStatus.Pending.Number.Equals(profileDto.VerificationStatus)
               || CommonConstants.VerificationStatus.Verified.Number.Equals(profileDto.VerificationStatus)
               || (userDto.Roles != null && userDto.Roles.Any(r => r.Equals(CommonConstants.ContributorsRoleName))))
           // user has submitted the verification form (uploaded CV)
           && ((!string.IsNullOrEmpty(profileDto.DocumentCvId)) || (!string.IsNullOrEmpty(profileDto.LinkedInUrl)));

    private static bool IsVerifiedAccount(PublicProfileDto.PublicProfileDtoData publicProfileDto)
        => publicProfileDto.Roles != null && publicProfileDto.Roles.Any(r =>
                                              r.Equals(CommonConstants.ContributorsRoleName))
                                          && CommonConstants.VerificationStatus.Verified.Text.Equals(publicProfileDto
                                              .VerificationStatus);

    private static bool IsPendingVerification(PublicProfileDto.PublicProfileDtoData publicProfileDto, string documentCvId, string linkedInUrl)
        => !IsVerifiedAccount(publicProfileDto)
           && (CommonConstants.VerificationStatus.Pending.Text.Equals(publicProfileDto.VerificationStatus)
               || CommonConstants.VerificationStatus.Verified.Text.Equals(publicProfileDto.VerificationStatus)
               || (publicProfileDto.Roles != null &&
                   publicProfileDto.Roles.Any(r => r.Equals(CommonConstants.ContributorsRoleName))))
          // user has submitted the verification form (uploaded CV)
          && ((!string.IsNullOrEmpty(documentCvId)) || (!string.IsNullOrEmpty(linkedInUrl)));
}