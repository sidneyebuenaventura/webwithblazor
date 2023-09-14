using DigitalHubUI.ViewModels;
using Progress.Sitefinity.RestSdk.Dto;

namespace DigitalHubUI.Mappers;

public static class ImageMapper
{
    private const string CoverPictureThumbnailName = "cover300";
    private const string thumbnailName = "thumbnail";

    public static ImageViewModel? Map(ImageDto? image, Boolean resize=false)
    {
        if (image == null)
            return null;

        var imageUrl = image.Url;

        var thumbnailDto = image.Thumbnails?.FirstOrDefault(t => t.Title.Equals((resize)?thumbnailName:CoverPictureThumbnailName));
        if (thumbnailDto != null)
        {
            imageUrl = thumbnailDto.Url;
        }

        return new ImageViewModel(imageUrl);
    }
}