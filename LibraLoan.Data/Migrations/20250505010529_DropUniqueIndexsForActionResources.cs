using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class DropUniqueIndexsForActionResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Action",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Resource",
                table: "Permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Action",
                table: "Permissions",
                column: "Action",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Resource",
                table: "Permissions",
                column: "Resource",
                unique: true);
        }
    }
}
