using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace PostgreSearch.Migrations
{
	/// <inheritdoc />
	public partial class ArticleLocalizationIndex : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVector",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false)
				.Annotation("Npgsql:TsVectorConfig", "english")
				.Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" });

			migrationBuilder.CreateIndex(
				name: "IX_ArticleLocalizations_SearchVector",
				table: "ArticleLocalizations",
				column: "SearchVector")
				.Annotation("Npgsql:IndexMethod", "GIN");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_ArticleLocalizations_SearchVector",
				table: "ArticleLocalizations");

			migrationBuilder.DropColumn(
				name: "SearchVector",
				table: "ArticleLocalizations");
		}
	}
}
