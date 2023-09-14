namespace DigitalHubUI.Dto.Course;

public class CreateModuleRequestDto
{
	public string Title { get; }
	public string? Content { get; }
	public int Order { get; }
	public int? EstimatedDuration { get; }
	
	public CreateModuleRequestDto(string title, string? content, int order, int? estimatedDuration)
	{
		Title = title;
		Content = content;
		Order = order;
		EstimatedDuration = estimatedDuration;
	}

	public ModuleDto ToModuleDto(string parentId)
	{
		return new ModuleDto
		{
			Title = Title,
			Content = Content,
			Order = Order,
			EstimatedDuration = EstimatedDuration,
			ParentId = parentId
		};
	}
}

