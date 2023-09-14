using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk.Dto.Content;

namespace DigitalHubUI.Dto;

[MappedSitefinityType("Telerik.Sitefinity.Pages.Model.PageNode")]
public class PageNodeDto : ContentWithParentDto
{
    public string Id{ get; set; }
    public string Title { get; set; }
    public string LastUpdateDate { get; set; }
}