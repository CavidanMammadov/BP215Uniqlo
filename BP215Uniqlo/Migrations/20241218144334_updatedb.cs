using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BP215Uniqlo.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoductRatings_AspNetUsers_UserId",
                table: "PoductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_PoductRatings_Product_ProductId",
                table: "PoductRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PoductRatings",
                table: "PoductRatings");

            migrationBuilder.RenameTable(
                name: "PoductRatings",
                newName: "ProductRatings");

            migrationBuilder.RenameIndex(
                name: "IX_PoductRatings_UserId",
                table: "ProductRatings",
                newName: "IX_ProductRatings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PoductRatings_ProductId",
                table: "ProductRatings",
                newName: "IX_ProductRatings_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRatings",
                table: "ProductRatings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    USerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComment_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductComment_ProductId",
                table: "ProductComment",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_AspNetUsers_UserId",
                table: "ProductRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_Product_ProductId",
                table: "ProductRatings",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_AspNetUsers_UserId",
                table: "ProductRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_Product_ProductId",
                table: "ProductRatings");

            migrationBuilder.DropTable(
                name: "ProductComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRatings",
                table: "ProductRatings");

            migrationBuilder.RenameTable(
                name: "ProductRatings",
                newName: "PoductRatings");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRatings_UserId",
                table: "PoductRatings",
                newName: "IX_PoductRatings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductRatings_ProductId",
                table: "PoductRatings",
                newName: "IX_PoductRatings_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PoductRatings",
                table: "PoductRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PoductRatings_AspNetUsers_UserId",
                table: "PoductRatings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PoductRatings_Product_ProductId",
                table: "PoductRatings",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
