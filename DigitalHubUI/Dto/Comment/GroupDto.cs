namespace DigitalHubUI.Dto.Comment;

public class GroupDto
{
	public string Key { get; }
	public string Name { get; }
	public string Description { get; }
	
	public GroupDto(string key, string name, string description)
	{
		Key = key;
		Name = name;
		Description = description;
	}
}
