using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class DetailedCourseViewComponentConfiguration
{
    [DisplayName("Number of related courses to display")]
    public int RelatedItemsCount { get; set; }

    public DetailedCourseViewComponentConfiguration()
    {
        RelatedItemsCount = 4;
    }
}