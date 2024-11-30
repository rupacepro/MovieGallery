using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieGallery.Migrations
{
    /// <inheritdoc />
    public partial class reviewpopulated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CreatedAt", "MovieId", "UserId" },
                values: new object[,]
                {
                    { 1, "This movie is fantastic, loved it!", new DateTime(2024, 11, 29, 2, 10, 18, 468, DateTimeKind.Utc).AddTicks(2017), 1, "7f8ca75a-80c6-4790-87f8-a230519eff38" },
                    { 2, "I will rate this movie 5 out of 5.", new DateTime(2024, 11, 29, 2, 10, 18, 468, DateTimeKind.Utc).AddTicks(2021), 1, "8b5cd3b9-650e-4909-8734-75a3bcec7fac" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
