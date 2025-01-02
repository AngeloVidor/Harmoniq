using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ApplyingShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartEntity_Albums_AlbumId",
                table: "CartEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CartEntity_ContentConsumers_ConsumerId",
                table: "CartEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartEntity",
                table: "CartEntity");

            migrationBuilder.RenameTable(
                name: "CartEntity",
                newName: "ShoppingCart");

            migrationBuilder.RenameIndex(
                name: "IX_CartEntity_ConsumerId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_CartEntity_AlbumId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Albums_AlbumId",
                table: "ShoppingCart",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_ContentConsumers_ConsumerId",
                table: "ShoppingCart",
                column: "ConsumerId",
                principalTable: "ContentConsumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Albums_AlbumId",
                table: "ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_ContentConsumers_ConsumerId",
                table: "ShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart");

            migrationBuilder.RenameTable(
                name: "ShoppingCart",
                newName: "CartEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_ConsumerId",
                table: "CartEntity",
                newName: "IX_CartEntity_ConsumerId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_AlbumId",
                table: "CartEntity",
                newName: "IX_CartEntity_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartEntity",
                table: "CartEntity",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartEntity_Albums_AlbumId",
                table: "CartEntity",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartEntity_ContentConsumers_ConsumerId",
                table: "CartEntity",
                column: "ConsumerId",
                principalTable: "ContentConsumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
