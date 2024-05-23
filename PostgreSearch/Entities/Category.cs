namespace PostgreSearch.Entities;

public class Category
{
	public long Id { get; set; }

	public List<CategoryLocalizations>? CategoryLocalizations { get; set; }
	public List<Article>? Articles { get; set; }
}
