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

	public void OnGet(string? query = null, Languages? language = null, long? categoryId = null)
	{
		Query = query;
		Language = language ?? Languages.All;
		CategoryId = categoryId;

		SetCategories();
		SetArticles();
	}

	private void SetCategories()
	{
		Categories = context.Set<Category>()
			.AsQueryable()
			.Include(x => x.CategoryLocalizations)
			.Select(x => new CategoryModel(x.Id, x.CategoryLocalizations![0].Title))
			.ToList();
	}
		
	private void SetArticles()
	{
		IQueryable<ArticleLocalizations> articlesQueryable = context.Set<ArticleLocalizations>()
			.AsQueryable()
			.Include(x => x.Article);

		if (!string.IsNullOrWhiteSpace(Query))
		{
			articlesQueryable = articlesQueryable
				.Where(x => x.SearchVector.Matches(
					EF.Functions.PlainToTsQuery(x.Language == Languages.English ? "english" : "ukrainian", Query)))
				.OrderByDescending(x => x.SearchVector.Rank(
					EF.Functions.PlainToTsQuery(x.Language == Languages.English ? "english" : "ukrainian", Query)));
		}

		if (Language is not Languages.All)
		{
			articlesQueryable = articlesQueryable.Where(x => x.Language == Language);
		}

		if (CategoryId is not null)
		{
			articlesQueryable = articlesQueryable.Where(x => x.Article!.CategoryId == CategoryId);
		}

		Articles = articlesQueryable
			.Select(x => new ArticleModel(x.Title, x.Content, x.Language, x.Article!.CategoryId))
			.ToList();
	}
}
