using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ContentCreatorIdInSongsAlbumTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentCreatorId",
                table: "AlbumSongs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentCreatorId",
                table: "AlbumSongs");
        }
    }
}
