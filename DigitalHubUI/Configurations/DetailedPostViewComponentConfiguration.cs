using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class DetailedPostViewComponentConfiguration
{
    [DisplayName("Number of related articles to display")]
    public int RelatedItemsCount { get; set; }

    public DetailedPostViewComponentConfiguration()
    {
        RelatedItemsCount = 4;
    }
}