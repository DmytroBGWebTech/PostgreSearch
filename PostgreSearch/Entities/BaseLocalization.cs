using PostgreSearch.Models;

namespace PostgreSearch.Entities;

public class BaseLocalization
{
	public long Id { get; set; }
	public required string Title { get; set; }
	public required Languages Language { get; set; }
}
