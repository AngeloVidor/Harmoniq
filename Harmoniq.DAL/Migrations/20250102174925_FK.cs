using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_ContentConsumers_ConsumerId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ConsumerId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                table: "ShoppingCart");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ContentConsumerId",
                table: "ShoppingCart",
                column: "ContentConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_ContentConsumers_ContentConsumerId",
                table: "ShoppingCart",
                column: "ContentConsumerId",
                principalTable: "ContentConsumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_ContentConsumers_ContentConsumerId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ContentConsumerId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<int>(
                name: "ConsumerId",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ConsumerId",
                table: "ShoppingCart",
                column: "ConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_ContentConsumers_ConsumerId",
                table: "ShoppingCart",
                column: "ConsumerId",
                principalTable: "ContentConsumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
