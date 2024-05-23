using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace PostgreSearch.Migrations
{
	/// <inheritdoc />
	public partial class RemoveCombined : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "SearchVectorCombined",
				table: "ArticleLocalizations");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<NpgsqlTsVector>(
				name: "SearchVectorCombined",
				table: "ArticleLocalizations",
				type: "tsvector",
				nullable: false,
				computedColumnSql: "to_tsvector('english', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", '')) || to_tsvector('ukrainian', coalesce(\"Title\", '') || ' ' || coalesce(\"Content\", ''))",
				stored: true);
		}
	}
}
