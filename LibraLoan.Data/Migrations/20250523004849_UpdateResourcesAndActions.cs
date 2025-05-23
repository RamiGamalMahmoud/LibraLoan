using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResourcesAndActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "الادارة");

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "DateCreated", "Name" },
                values: new object[] { 7, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "العملاء" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "إدارة");
        }
    }
}
