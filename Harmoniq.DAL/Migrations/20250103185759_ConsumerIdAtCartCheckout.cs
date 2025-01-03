using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConsumerIdAtCartCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentConsumerId",
                table: "CartCheckout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartCheckout_ContentConsumerId",
                table: "CartCheckout",
                column: "ContentConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartCheckout_ContentConsumers_ContentConsumerId",
                table: "CartCheckout",
                column: "ContentConsumerId",
                principalTable: "ContentConsumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartCheckout_ContentConsumers_ContentConsumerId",
                table: "CartCheckout");

            migrationBuilder.DropIndex(
                name: "IX_CartCheckout_ContentConsumerId",
                table: "CartCheckout");

            migrationBuilder.DropColumn(
                name: "ContentConsumerId",
                table: "CartCheckout");
        }
    }
}
