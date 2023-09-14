using System.ComponentModel;

namespace DigitalHubUI.Configurations;

public class CourseBuilderConfiguration
{
	[DisplayName("Message when validating that required field is not filled")]
	public string ValidationRequiredMessage { get; set; }
	
	public bool IsEdit { get; set; }

	public CourseBuilderConfiguration()
	{
		ValidationRequiredMessage = "This field is required";
		IsEdit = false;
	}
}
