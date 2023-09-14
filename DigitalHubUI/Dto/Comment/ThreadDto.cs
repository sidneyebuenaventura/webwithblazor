namespace DigitalHubUI.Dto.Comment;

public class ThreadDto
{	
	public string Key { get; }
	public string Title { get; }
	public string Type { get; }
	public string Language { get; }
	public string GroupKey { get; }
	public string DataSource { get; }
	public GroupDto Group { get; }

	public ThreadDto(string key, string title)
	{
		Type = "Telerik.Sitefinity.Blogs.Model.BlogPost";
		Language = "en";
		GroupKey = "Telerik.Sitefinity.Modules.Blogs.BlogsManager_digitalhubblogs";
		DataSource = "OpenAccessDataProvider";
		Key = key;
		Title = title;
		Group = new GroupDto(GroupKey, "Blog posts", title);
	}

}
