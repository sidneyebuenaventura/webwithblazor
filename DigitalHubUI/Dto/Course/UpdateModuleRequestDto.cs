namespace DigitalHubUI.Dto.Course;

public class UpdateModuleRequestDto
{
	public string? Id { get; }
	public string Title { get; }
	public string? Content { get; }
	public int Order { get; }
	public int? EstimatedDuration { get; }
	public bool? ToDelete { get; }
	
	public UpdateModuleRequestDto(string id, string title, string? content, int order, int? estimatedDuration,
		bool? toDelete)
	{
		Id = id;
		Title = title;
		Content = content;
		Order = order;
		EstimatedDuration = estimatedDuration;
		ToDelete = toDelete;
	}

	public ModuleDto ToModuleDto(string parentId)
	{
		return new ModuleDto
		{
			Id = string.IsNullOrEmpty(Id) ? null : Id,
			Title = Title,
			Content = Content,
			Order = Order,
			EstimatedDuration = EstimatedDuration,
			ParentId = parentId
		};
	}
}

