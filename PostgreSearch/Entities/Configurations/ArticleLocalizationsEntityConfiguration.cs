using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgreSearch.Entities.Configurations;

public class ArticleLocalizationsEntityConfiguration : IEntityTypeConfiguration<ArticleLocalizations>
{
	public void Configure(EntityTypeBuilder<ArticleLocalizations> builder)
	{
		builder
			.Property(x => x.SearchVector)
			.HasComputedColumnSql(
				"CASE WHEN \"Language\" = 0 THEN to_tsvector('english', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", ''))" +
				"WHEN \"Language\" = 1 THEN to_tsvector('ukrainian', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", ''))" +
				" ELSE NULL END",
				stored: true);

		builder
			.HasIndex(b => new { b.Title, b.Content })
			.HasMethod("GIN")
			.IsTsVectorExpressionIndex("english");
	}
}
