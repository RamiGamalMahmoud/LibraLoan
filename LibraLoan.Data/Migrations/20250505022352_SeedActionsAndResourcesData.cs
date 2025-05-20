using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedActionsAndResourcesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "DateCreated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "إنشاء" },
                    { 2, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "قراءة" },
                    { 3, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "تحديث" },
                    { 4, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "حذف" }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "DateCreated", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "المستخدمين" },
                    { 2, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "إدارة" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
