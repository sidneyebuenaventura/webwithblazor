using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class FeaturedCoursesViewComponentConfiguration
{
	[DisplayName("Number of items to display")]
	public int ItemCount { get; set; }

	public FeaturedCoursesViewComponentConfiguration()
	{
		ItemCount = 4;
	}
}