using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class CoursesFilteredByCategoryListViewComponentConfiguration
{
    [DisplayName("Number of courses to display")]
    public int InitItemCount { get; set; }
    
    [DisplayName("Number of items to load when clicking 'see more'")]
    public int SeeMoreItemCount { get; set; }

    public CoursesFilteredByCategoryListViewComponentConfiguration()
    {
        SeeMoreItemCount = 8;
        InitItemCount = 8;
    }
}