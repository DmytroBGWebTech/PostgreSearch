namespace PostgreSearch.Models;

public record ArticleModel(string Title, string Content, Languages Language, long CategoryId);
