using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedById", "DateCreated", "Password", "Username" },
                values: new object[] { 1, null, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$Sk2NHXsX3PrWVTrgfmEfa.xw2ViWvadOEFR1leLTq2jSUzu4ak5.2", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
