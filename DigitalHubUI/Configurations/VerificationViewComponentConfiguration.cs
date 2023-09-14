using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class VerificationViewComponentConfiguration
{
	[DisplayName("Message when validating that required field is not filled")]
	public string ValidationRequiredMessage { get; set; }
	
	public VerificationViewComponentConfiguration()
	{
		ValidationRequiredMessage = "This field is required";
	}
}