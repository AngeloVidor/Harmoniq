using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangingCartStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Albums_AlbumId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_AlbumId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "ShoppingCart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_AlbumId",
                table: "ShoppingCart",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Albums_AlbumId",
                table: "ShoppingCart",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
