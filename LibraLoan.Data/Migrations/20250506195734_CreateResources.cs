using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "DateCreated", "Name" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "المؤلفون" },
                    { 4, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "دور النشر" },
                    { 5, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الكتب" },
                    { 6, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "الاستعارة" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
