using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPublisherCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Publishers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_CreatedById",
                table: "Publishers",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Publishers_Users_CreatedById",
                table: "Publishers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publishers_Users_CreatedById",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_CreatedById",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Publishers");
        }
    }
}
