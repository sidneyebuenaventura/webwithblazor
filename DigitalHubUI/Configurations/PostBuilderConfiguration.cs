using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class PostBuilderConfiguration
{
	[DisplayName("Message when validating that required field is not filled")]
	public string ValidationRequiredMessage { get; set; }
	
	public bool IsEdit { get; set; }

	public PostBuilderConfiguration()
	{
		ValidationRequiredMessage = "This field is required";
		IsEdit = false;
	}
}
