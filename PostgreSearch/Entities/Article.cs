namespace PostgreSearch.Entities;

public class Article
{
	public long Id { get; set; }

	public required long CategoryId { get; set; }
	public List<ArticleLocalizations>? ArticleLocalizations { get; set; }
	public Category? Category { get; set; }
}
