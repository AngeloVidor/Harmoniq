using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteAlbums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteAlbums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    ContentConsumerId = table.Column<int>(type: "int", nullable: false),
                    DateFavorited = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FavoriteAlbums_ContentConsumers_ContentConsumerId",
                        column: x => x.ContentConsumerId,
                        principalTable: "ContentConsumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAlbums_AlbumId",
                table: "FavoriteAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAlbums_ContentConsumerId",
                table: "FavoriteAlbums",
                column: "ContentConsumerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteAlbums");
        }
    }
}
