using Newtonsoft.Json;

namespace DigitalHubUI.Dto.Comment.Api;

public class QueryCommentApiResponseDto
{
	[JsonProperty("comments")]
	public List<CustomCommentDto> Items { get; }
	
	public QueryCommentApiResponseDto(List<CustomCommentDto> items)
	{
		Items = items;
	}
}
