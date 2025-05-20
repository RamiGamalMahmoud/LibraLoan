using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class CleanPermissonsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Resource",
                table: "Permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Permissions",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resource",
                table: "Permissions",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Action", "DateCreated", "Resource" },
                values: new object[,]
                {
                    { 1, "إنشاء", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "المستخدمين" },
                    { 2, "قراءة", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "المستخدمين" },
                    { 3, "تحديث", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "المستخدمين" },
                    { 4, "حذف", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "المستخدمين" },
                    { 5, "قراءة", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "إدارة" }
                });
        }
    }
}
