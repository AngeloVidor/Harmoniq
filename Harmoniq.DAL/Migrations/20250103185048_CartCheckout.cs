using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CartCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartCheckoutEntityId",
                table: "CartAlbums",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartCheckout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartCheckout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartCheckout_CartAlbums_CartId",
                        column: x => x.CartId,
                        principalTable: "CartAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartAlbums_CartCheckoutEntityId",
                table: "CartAlbums",
                column: "CartCheckoutEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CartCheckout_CartId",
                table: "CartCheckout",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartAlbums_CartCheckout_CartCheckoutEntityId",
                table: "CartAlbums",
                column: "CartCheckoutEntityId",
                principalTable: "CartCheckout",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartAlbums_CartCheckout_CartCheckoutEntityId",
                table: "CartAlbums");

            migrationBuilder.DropTable(
                name: "CartCheckout");

            migrationBuilder.DropIndex(
                name: "IX_CartAlbums_CartCheckoutEntityId",
                table: "CartAlbums");

            migrationBuilder.DropColumn(
                name: "CartCheckoutEntityId",
                table: "CartAlbums");
        }
    }
}
