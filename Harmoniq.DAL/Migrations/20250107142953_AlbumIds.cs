using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlbumIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "CartCheckout");

            migrationBuilder.AddColumn<string>(
                name: "AlbumIds",
                table: "CartCheckout",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumIds",
                table: "CartCheckout");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "CartCheckout",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
