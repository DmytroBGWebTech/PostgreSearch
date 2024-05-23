using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace PostgreSearch.Migrations
{
	/// <inheritdoc />
	public partial class TestComputedSql : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_ArticleLocalizations_SearchVectorEnglish",
				table: "ArticleLocalizations");

			migrationBuilder.DropIndex(
				name: "IX_ArticleLocalizations_SearchVectorUkrainian",
				table: "ArticleLocalizations");

			migrationBuilder.DropColumn(
				name: "SearchVectorEnglish",
				table: "ArticleLocalizations");

			migrationBuilder.DropColumn(
				name: "SearchVectorUkrainian",
				table: "ArticleLocalizations");

			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVector",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false,
				computedColumnSql: "CASE WHEN \"Language\" = 0 THEN to_tsvector('english', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", ''))WHEN \"Language\" = 1 THEN to_tsvector('ukrainian', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", '')) ELSE NULL END",
				stored: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "SearchVector",
				table: "ArticleLocalizations");

			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVectorEnglish",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false)
				.Annotation("Npgsql:TsVectorConfig", "english")
				.Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" });

			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVectorUkrainian",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false)
				.Annotation("Npgsql:TsVectorConfig", "ukrainian")
				.Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" });

			migrationBuilder.CreateIndex(
				name: "IX_ArticleLocalizations_SearchVectorEnglish",
				table: "ArticleLocalizations",
				column: "SearchVectorEnglish")
				.Annotation("Npgsql:IndexMethod", "GIN");

			migrationBuilder.CreateIndex(
				name: "IX_ArticleLocalizations_SearchVectorUkrainian",
				table: "ArticleLocalizations",
				column: "SearchVectorUkrainian")
				.Annotation("Npgsql:IndexMethod", "GIN");
		}
	}
}
