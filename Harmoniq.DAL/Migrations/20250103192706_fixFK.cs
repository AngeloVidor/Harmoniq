using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartCheckout_CartAlbums_CartId",
                table: "CartCheckout");

            migrationBuilder.AddForeignKey(
                name: "FK_CartCheckout_ShoppingCart_CartId",
                table: "CartCheckout",
                column: "CartId",
                principalTable: "ShoppingCart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartCheckout_ShoppingCart_CartId",
                table: "CartCheckout");

            migrationBuilder.AddForeignKey(
                name: "FK_CartCheckout_CartAlbums_CartId",
                table: "CartCheckout",
                column: "CartId",
                principalTable: "CartAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
