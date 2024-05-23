using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgreSearch.Entities.Configurations;

public class ArticleEntityConfiguration : IEntityTypeConfiguration<Article>
{
	public void Configure(EntityTypeBuilder<Article> builder)
	{
		builder
			.HasOne(a => a.Category)
			.WithMany(c => c.Articles)
			.HasForeignKey(a => a.CategoryId)
			.OnDelete(DeleteBehavior.Restrict);

		builder
			.HasMany(a => a.ArticleLocalizations)
			.WithOne(al => al.Article)
			.HasForeignKey(al => al.ArticleId);
	}
}
