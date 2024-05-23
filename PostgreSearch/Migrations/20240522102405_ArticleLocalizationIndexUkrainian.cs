using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace PostgreSearch.Migrations
{
	/// <inheritdoc />
	public partial class ArticleLocalizationIndexUkrainian : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<NpgsqlTsVector>(
				name: "SearchVector",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false,
				oldClrType: typeof(NpgsqlTsVector),
				oldType: "tsvector")
				.Annotation("Npgsql:TsVectorConfig", "ukrainian")
				.Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" })
				.OldAnnotation("Npgsql:TsVectorConfig", "english")
				.OldAnnotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<NpgsqlTsVector>(
				name: "SearchVector",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false,
				oldClrType: typeof(NpgsqlTsVector),
				oldType: "tsvector")
				.Annotation("Npgsql:TsVectorConfig", "english")
				.Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" })
				.OldAnnotation("Npgsql:TsVectorConfig", "ukrainian")
				.OldAnnotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" });
		}
	}
}
