using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionsActionAndRsourceColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "Permissions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Permissions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ActionId",
                table: "Permissions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Actions_ActionId",
                table: "Permissions",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Resources_ResourceId",
                table: "Permissions",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Actions_ActionId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Resources_ResourceId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ActionId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Permissions");
        }
    }
}
