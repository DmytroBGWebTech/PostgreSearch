using System.Diagnostics;
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
		var st = new Stopwatch();
		st.Start();
		await SetArticlesAsync();
		st.Stop();
		Console.WriteLine(st.ElapsedMilliseconds);
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
			//var prefixQuery = string.Join(" & ", Query.Split(' ').Select(term => term + ":*"));
			articlesQueryable = articlesQueryable
				.Select(x => new
				{
					Article = x,
					//TsQuery = EF.Functions.ToTsQuery(x.Language == Languages.English ? "english" : "ukrainian", prefixQuery),
					TsQuery = EF.Functions.PlainToTsQuery(x.Language == Languages.English ? "english" : "ukrainian", Query)
				})
				.Where(x => x.Article.SearchVector.Matches(x.TsQuery))
				.OrderByDescending(x => x.Article.SearchVector.Rank(x.TsQuery))
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
			//.Take(take)
			.ToListAsync();
	}
}
