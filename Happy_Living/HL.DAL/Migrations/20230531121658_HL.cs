using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HL.DAL.Migrations
{
    public partial class HL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComPassword",
                table: "RegisterTable");

            migrationBuilder.RenameColumn(
                name: "Confirmpassword",
                table: "PGAdminRegisters",
                newName: "Hashpassword");

            migrationBuilder.CreateTable(
                name: "SuperAdminClass",
                columns: table => new
                {
                    SuperAdmin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hashpassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdminClass", x => x.SuperAdmin_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperAdminClass");

            migrationBuilder.RenameColumn(
                name: "Hashpassword",
                table: "PGAdminRegisters",
                newName: "Confirmpassword");

            migrationBuilder.AddColumn<string>(
                name: "ComPassword",
                table: "RegisterTable",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
