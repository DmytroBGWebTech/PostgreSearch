using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace PostgreSearch.Migrations
{
	/// <inheritdoc />
	public partial class ExpressionIndex : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "SearchVector",
				table: "ArticleLocalizations");

			migrationBuilder.Sql(@"
				CREATE INDEX fts_idx ON ""ArticleLocalizations"" 
				USING GIN (
	            to_tsvector(
	                CASE 
	                    WHEN ""Language"" = 0 THEN 'english'::regconfig
	                    WHEN ""Language"" = 1 THEN 'ukrainian'::regconfig
	                    ELSE 'simple'::regconfig
	                END, ""Title"" || ' ' || ""Content"")
				);");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVector",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false,
				computedColumnSql: "CASE WHEN \"Language\" = 0 THEN to_tsvector('english', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", ''))WHEN \"Language\" = 1 THEN to_tsvector('ukrainian', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", '')) ELSE NULL END",
				stored: true);

			migrationBuilder.Sql(@"DROP INDEX fts_idx;");
		}
	}
}
