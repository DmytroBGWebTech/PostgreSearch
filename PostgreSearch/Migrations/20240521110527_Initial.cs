using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PostgreSearch.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Categories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Articles",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					CategoryId = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Articles", x => x.Id);
					table.ForeignKey(
						name: "FK_Articles_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "CategoryLocalizations",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					CategoryId = table.Column<long>(type: "bigint", nullable: false),
					Title = table.Column<string>(type: "text", nullable: false),
					Language = table.Column<byte>(type: "smallint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CategoryLocalizations", x => x.Id);
					table.ForeignKey(
						name: "FK_CategoryLocalizations_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ArticleLocalizations",
				columns: table => new
				{
					Id = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Content = table.Column<string>(type: "text", nullable: false),
					ArticleId = table.Column<long>(type: "bigint", nullable: false),
					Title = table.Column<string>(type: "text", nullable: false),
					Language = table.Column<byte>(type: "smallint", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ArticleLocalizations", x => x.Id);
					table.ForeignKey(
						name: "FK_ArticleLocalizations_Articles_ArticleId",
						column: x => x.ArticleId,
						principalTable: "Articles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_ArticleLocalizations_ArticleId",
				table: "ArticleLocalizations",
				column: "ArticleId");

			migrationBuilder.CreateIndex(
				name: "IX_Articles_CategoryId",
				table: "Articles",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_CategoryLocalizations_CategoryId",
				table: "CategoryLocalizations",
				column: "CategoryId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ArticleLocalizations");

			migrationBuilder.DropTable(
				name: "CategoryLocalizations");

			migrationBuilder.DropTable(
				name: "Articles");

			migrationBuilder.DropTable(
				name: "Categories");
		}
	}
}
