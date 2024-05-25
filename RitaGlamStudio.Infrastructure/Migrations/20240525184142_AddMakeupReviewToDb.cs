using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RitaGlamStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMakeupReviewToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MakeupReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakeupProductId = table.Column<int>(type: "int", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Review = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakeupReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MakeupReviews_MakeupProducts_MakeupProductId",
                        column: x => x.MakeupProductId,
                        principalTable: "MakeupProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MakeupReviews",
                columns: new[] { "Id", "ClientName", "MakeupProductId", "Rating", "Review", "ReviewDate" },
                values: new object[,]
                {
                    { 1, "Elisabeth Smith", 1, 5, "This lipstick is my all time favorite.", new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Joana Higgins", 2, 4, "This mascara is great, but it smudges a little.", new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Jane Roe", 3, 5, "The foundation has a perfect match for my skin tone.", new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Alice Johnson", 4, 3, "The concealer works well but is a bit pricey.", new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Bob Brown", 5, 5, "The eyeshadow palette has amazing colors and great pigmentation.", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MakeupReviews_MakeupProductId",
                table: "MakeupReviews",
                column: "MakeupProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MakeupReviews");
        }
    }
}
