using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostgreSearch.Entities;
using PostgreSearch.Models;

namespace PostgreSearch.Pages;
public class IndexModel(ApplicationDbContext context) : PageModel
{
	public List<ArticleModel> Articles { get; set; } = [];
	public List<CategoryModel> Categories { get; set; } = [];

	public string? Query { get; set; }
	public Languages? Language { get; set; }
	public long? CategoryId { get; set; }

	public async Task OnGetAsync(string? query = null, Languages? language = null, long? categoryId = null)
	{
		Query = query;
		Language = language ?? Languages.All;
		CategoryId = categoryId;

		await SetCategoriesAsync();
		await SetArticlesAsync();
	}

	private async Task SetCategoriesAsync()
	{
		Categories = await context.Set<Category>()
			.AsQueryable()
			.Include(x => x.CategoryLocalizations)
			.Select(x => new CategoryModel(x.Id, x.CategoryLocalizations![0].Title))
			.ToListAsync();
	}

	private async Task SetArticlesAsync(int take = 10)
	{
		IQueryable<ArticleLocalizations> articlesQueryable = context.Set<ArticleLocalizations>()
			.AsQueryable()
			.Include(x => x.Article);

		if (!string.IsNullOrWhiteSpace(Query))
		{
			articlesQueryable = articlesQueryable
				.Select(x => new
				{
					Article = x,
					TsVector = EF.Functions.ToTsVector(x.Language == Languages.English ? "english" : "ukrainian", x.Title + " " + x.Content),
					TsQuery = EF.Functions.PlainToTsQuery(x.Language == Languages.English ? "english" : "ukrainian", Query)
				})
				.Where(x => x.TsVector.Matches(x.TsQuery))
				.OrderByDescending(x => x.TsVector.Rank(x.TsQuery))
				.Select(x => x.Article);
		}

		if (Language is not Languages.All)
		{
			articlesQueryable = articlesQueryable.Where(x => x.Language == Language);
		}

		if (CategoryId is not null)
		{
			articlesQueryable = articlesQueryable.Where(x => x.Article!.CategoryId == CategoryId);
		}

		Articles = await articlesQueryable
			.Select(x => new ArticleModel(x.Title, x.Content, x.Language, x.Article!.CategoryId))
			.Take(take)
			.ToListAsync();
	}
}
