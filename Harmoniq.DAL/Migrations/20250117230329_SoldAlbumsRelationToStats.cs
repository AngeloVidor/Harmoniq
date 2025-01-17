using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SoldAlbumsRelationToStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticsAlbums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    ContentCreatorId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticsAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StatisticsAlbums_ContentCreators_ContentCreatorId",
                        column: x => x.ContentCreatorId,
                        principalTable: "ContentCreators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsAlbums_AlbumId",
                table: "StatisticsAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsAlbums_ContentCreatorId",
                table: "StatisticsAlbums",
                column: "ContentCreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticsAlbums");
        }
    }
}
