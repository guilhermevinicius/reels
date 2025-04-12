using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reels.Backoffice.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genre", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GenreCategory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genre_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_genre_category_genre_genre_id",
                        column: x => x.genre_id,
                        principalTable: "Genre",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_genre_id",
                table: "Genre",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_genre_category_genre_id_category_id",
                table: "GenreCategory",
                columns: new[] { "genre_id", "category_id" });

            migrationBuilder.CreateIndex(
                name: "ix_genre_category_id",
                table: "GenreCategory",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreCategory");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
