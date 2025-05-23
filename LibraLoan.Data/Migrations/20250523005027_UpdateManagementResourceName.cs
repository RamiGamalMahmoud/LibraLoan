using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManagementResourceName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "الإدارة");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "الادارة");
        }
    }
}
