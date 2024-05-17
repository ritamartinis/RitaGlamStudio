using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RitaGlamStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMakeupProductToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "MakeupProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakeupProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MakeupProducts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MakeupProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MakeupProducts",
                columns: new[] { "Id", "BrandId", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 3, "A vibrant, matte, red lipstick.", "https://example.com/mac-ruby-woo.jpg", "MAC Ruby Woo Lipstick", 27, 50 },
                    { 2, 2, 2, "A lengthening and volumizing mascara.", "https://example.com/maybelline-lash-sensational.jpg", "Maybelline Lash Sensational Mascara", 12, 75 },
                    { 3, 3, 1, "A blendable foundation that matches skin tone.", "https://example.com/loreal-true-match.jpg", "L'Oréal Paris True Match Foundation", 15, 130 },
                    { 4, 4, 1, "A creamy concealer with radiant finish.", "https://example.com/nars-creamy-concealer.jpg", "NARS Radiant Creamy Concealer", 30, 190 },
                    { 5, 5, 2, "A versatile eyeshadow palette with neutral shades.", "https://example.com/urban-decay-naked.jpg", "Urban Decay Naked Eyeshadow Palette", 54, 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MakeupProducts_BrandId",
                table: "MakeupProducts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_MakeupProducts_CategoryId",
                table: "MakeupProducts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MakeupProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
