using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CartAlbums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartAlbums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CartAlbums_ShoppingCart_CartId",
                        column: x => x.CartId,
                        principalTable: "ShoppingCart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartAlbums_AlbumId",
                table: "CartAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAlbums_CartId",
                table: "CartAlbums",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartAlbums");
        }
    }
}
