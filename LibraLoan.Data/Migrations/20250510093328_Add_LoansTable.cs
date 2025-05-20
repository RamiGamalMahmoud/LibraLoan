using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_LoansTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsReturned = table.Column<bool>(type: "bit", nullable: true),
                    ActualReturnDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    LoanDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    ExpectedReturnDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
