using NpgsqlTypes;

namespace PostgreSearch.Entities;

public class ArticleLocalizations : BaseLocalization
{
	public required string Content { get; set; }

	public long ArticleId { get; set; }
	public Article? Article { get; set; }

	public NpgsqlTsVector SearchVector { get; set; } = null!;
}
