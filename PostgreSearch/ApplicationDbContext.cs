using Microsoft.EntityFrameworkCore;
using PostgreSearch.Entities;

namespace PostgreSearch;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public required DbSet<Category> Categories { get; set; }
	public required DbSet<Article> Articles { get; set; }
	public required DbSet<ArticleLocalizations> ArticleLocalizations { get; set; }
	public required DbSet<CategoryLocalizations> CategoryLocalizations { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
	}
}
