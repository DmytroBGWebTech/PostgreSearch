using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PostgreSearch.Entities.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder
			.HasMany(c => c.CategoryLocalizations)
			.WithOne(cl => cl.Category)
			.HasForeignKey(cl => cl.CategoryId);
	}
}
