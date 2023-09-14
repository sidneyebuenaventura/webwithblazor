using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class ArticlesFilteredByCategoryListViewComponentConfiguration
{
    [DisplayName("Number of articles to display")]
    public int InitItemCount { get; set; }
    
    [DisplayName("Number of items to load when clicking 'see more'")]
    public int SeeMoreItemCount { get; set; }

    public ArticlesFilteredByCategoryListViewComponentConfiguration()
    {
        SeeMoreItemCount = 8;
        InitItemCount = 8;
    }
}