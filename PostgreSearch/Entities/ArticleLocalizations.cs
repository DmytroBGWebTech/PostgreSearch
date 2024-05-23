using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace PostgreSearch.Entities;

public class ArticleLocalizations : BaseLocalization
{
	public required string Content { get; set; }

	public long ArticleId { get; set; }
	public Article? Article { get; set; }

	[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
	public NpgsqlTsVector? SearchVector { get; set; }
}
