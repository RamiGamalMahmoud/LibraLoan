using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnResourceAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ResourceId_ActionId",
                table: "Permissions",
                columns: new[] { "ResourceId", "ActionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_ResourceId_ActionId",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions",
                column: "ResourceId");
        }
    }
}
