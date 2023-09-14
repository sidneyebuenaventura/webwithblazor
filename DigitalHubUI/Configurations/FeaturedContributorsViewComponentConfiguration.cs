using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class FeaturedContributorsViewComponentConfiguration
{
	[DisplayName("The number of featured contributors to display")]
	public int FeaturedContributorsCount { get; set; }
	
	[DisplayName("True if we are displaying featured instructor instead of article author")]
	public bool IsFeaturedInstructor { get; set; }
	
	public FeaturedContributorsViewComponentConfiguration()
	{
		FeaturedContributorsCount = 5;
		IsFeaturedInstructor = false;
	}
}