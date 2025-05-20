using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraLoan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Photo = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Isbn = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Version = table.Column<int>(type: "INT", nullable: true),
                    PublishDate = table.Column<string>(type: "TEXT", nullable: false),
                    Copies = table.Column<int>(type: "INT", nullable: false, defaultValue: 1),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
