using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumSongs_Albums_AlbumEntityId",
                table: "AlbumSongs");

            migrationBuilder.DropIndex(
                name: "IX_AlbumSongs_AlbumEntityId",
                table: "AlbumSongs");

            migrationBuilder.DropColumn(
                name: "AlbumEntityId",
                table: "AlbumSongs");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "AlbumSongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSongs_AlbumId",
                table: "AlbumSongs",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumSongs_Albums_AlbumId",
                table: "AlbumSongs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumSongs_Albums_AlbumId",
                table: "AlbumSongs");

            migrationBuilder.DropIndex(
                name: "IX_AlbumSongs_AlbumId",
                table: "AlbumSongs");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "AlbumSongs");

            migrationBuilder.AddColumn<int>(
                name: "AlbumEntityId",
                table: "AlbumSongs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSongs_AlbumEntityId",
                table: "AlbumSongs",
                column: "AlbumEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumSongs_Albums_AlbumEntityId",
                table: "AlbumSongs",
                column: "AlbumEntityId",
                principalTable: "Albums",
                principalColumn: "Id");
        }
    }
}
