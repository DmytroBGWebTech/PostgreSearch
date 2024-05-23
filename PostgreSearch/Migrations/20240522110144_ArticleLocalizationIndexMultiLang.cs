using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace PostgreSearch.Migrations
{
	/// <inheritdoc />
	public partial class ArticleLocalizationIndexMultiLang : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "SearchVector",
				table: "ArticleLocalizations",
				newName: "SearchVectorUkrainian");

			migrationBuilder.RenameIndex(
				name: "IX_ArticleLocalizations_SearchVector",
				table: "ArticleLocalizations",
				newName: "IX_ArticleLocalizations_SearchVectorUkrainian");

			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVectorEnglish",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false)
				.Annotation("Npgsql:TsVectorConfig", "english")
				.Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" });

			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVectorCombined",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false,
				computedColumnSql: "to_tsvector('english', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", '')) || to_tsvector('ukrainian', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", ''))",
				stored: true);

			migrationBuilder.CreateIndex(
				name: "IX_ArticleLocalizations_SearchVectorEnglish",
				table: "ArticleLocalizations",
				column: "SearchVectorEnglish")
				.Annotation("Npgsql:IndexMethod", "GIN");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_ArticleLocalizations_SearchVectorEnglish",
				table: "ArticleLocalizations");

			migrationBuilder.DropColumn(
				name: "SearchVectorCombined",
				table: "ArticleLocalizations");

			migrationBuilder.DropColumn(
				name: "SearchVectorEnglish",
				table: "ArticleLocalizations");

			migrationBuilder.RenameColumn(
				name: "SearchVectorUkrainian",
				table: "ArticleLocalizations",
				newName: "SearchVector");

			migrationBuilder.RenameIndex(
				name: "IX_ArticleLocalizations_SearchVectorUkrainian",
				table: "ArticleLocalizations",
				newName: "IX_ArticleLocalizations_SearchVector");
		}
	}
}
