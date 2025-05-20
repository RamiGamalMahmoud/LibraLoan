using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Loans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_CreatedById",
                table: "Loans",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_CreatedById",
                table: "Loans",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_CreatedById",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_CreatedById",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Loans");
        }
    }
}
