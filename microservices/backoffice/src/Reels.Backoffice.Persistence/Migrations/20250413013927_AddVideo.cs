using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reels.Backoffice.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: false),
                    encoded_path = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_media", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    year_launched = table.Column<int>(type: "integer", nullable: false),
                    opened = table.Column<bool>(type: "boolean", nullable: false),
                    published = table.Column<bool>(type: "boolean", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    ThumbPath = table.Column<string>(type: "text", nullable: true),
                    thumbHalfPath = table.Column<string>(type: "text", nullable: true),
                    BannerPath = table.Column<string>(type: "text", nullable: true),
                    media_id = table.Column<Guid>(type: "uuid", nullable: true),
                    trailer_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_video", x => x.id);
                    table.ForeignKey(
                        name: "fk_video_media_media_id",
                        column: x => x.media_id,
                        principalTable: "Media",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_video_media_trailer_id",
                        column: x => x.trailer_id,
                        principalTable: "Media",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "VideoCategory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    video_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_video_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_video_category_video_video_id",
                        column: x => x.video_id,
                        principalTable: "Video",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGenre",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    video_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_video_genre", x => x.id);
                    table.ForeignKey(
                        name: "fk_video_genre_video_video_id",
                        column: x => x.video_id,
                        principalTable: "Video",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_media_id",
                table: "Media",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_video_id",
                table: "Video",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_video_media_id",
                table: "Video",
                column: "media_id");

            migrationBuilder.CreateIndex(
                name: "ix_video_trailer_id",
                table: "Video",
                column: "trailer_id");

            migrationBuilder.CreateIndex(
                name: "ix_video_category_id",
                table: "VideoCategory",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_video_category_video_id",
                table: "VideoCategory",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "ix_video_genre_id",
                table: "VideoGenre",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_video_genre_video_id",
                table: "VideoGenre",
                column: "video_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoCategory");

            migrationBuilder.DropTable(
                name: "VideoGenre");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}
