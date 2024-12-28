using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IsAlbumDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Albums",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Albums");
        }
    }
}
