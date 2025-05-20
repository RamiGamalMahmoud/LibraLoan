using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "DateCreated", "Resource" },
                values: new object[,]
                {
                    { 1, "Create", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Users" },
                    { 2, "Read", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Users" },
                    { 3, "Update", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Users" },
                    { 4, "Delete", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Users" },
                    { 5, "Read", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Management" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
