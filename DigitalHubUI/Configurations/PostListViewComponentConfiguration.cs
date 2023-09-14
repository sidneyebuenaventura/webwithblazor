using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class PostListViewComponentConfiguration
{
    [DisplayName("Number of items to display")]
    public int ItemCount { get; set; }

    [DisplayName("Display only feature articles")]
    public bool FeatureArticles { get; set; }

    public PostListViewComponentConfiguration()
    {
        ItemCount = 3;
        FeatureArticles = false;
    }
}