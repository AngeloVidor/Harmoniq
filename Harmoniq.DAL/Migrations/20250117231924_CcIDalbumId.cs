using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CcIDalbumId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Albums_AlbumId",
                table: "Stats");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_ContentCreators_ContentCreatorId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_AlbumId",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_ContentCreatorId",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "ContentCreatorId",
                table: "Stats");

            migrationBuilder.AddColumn<int>(
                name: "StatisticsId",
                table: "StatisticsAlbums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsAlbums_StatisticsId",
                table: "StatisticsAlbums",
                column: "StatisticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatisticsAlbums_Stats_StatisticsId",
                table: "StatisticsAlbums",
                column: "StatisticsId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatisticsAlbums_Stats_StatisticsId",
                table: "StatisticsAlbums");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsAlbums_StatisticsId",
                table: "StatisticsAlbums");

            migrationBuilder.DropColumn(
                name: "StatisticsId",
                table: "StatisticsAlbums");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Stats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContentCreatorId",
                table: "Stats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stats_AlbumId",
                table: "Stats",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_ContentCreatorId",
                table: "Stats",
                column: "ContentCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Albums_AlbumId",
                table: "Stats",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_ContentCreators_ContentCreatorId",
                table: "Stats",
                column: "ContentCreatorId",
                principalTable: "ContentCreators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
