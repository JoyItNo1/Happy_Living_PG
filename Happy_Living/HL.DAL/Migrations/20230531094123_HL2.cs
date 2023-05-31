using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HL.DAL.Migrations
{
    public partial class HL2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_date",
                table: "RegisterTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "PGAdminRegisters",
                columns: table => new
                {
                    PGAdmin_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Confirmpassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select_Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PG_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PG_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Payment_Methods = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role_Id = table.Column<int>(type: "int", nullable: true),
                    Is_Auth = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGAdminRegisters", x => x.PGAdmin_Id);
                });

            migrationBuilder.CreateTable(
                name: "PGsheringType",
                columns: table => new
                {
                    Sheretype_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGAdminId = table.Column<int>(type: "int", nullable: true),
                    SharingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGsheringType", x => x.Sheretype_Id);
                });

            migrationBuilder.CreateTable(
                name: "PGTypes",
                columns: table => new
                {
                    PGTypes_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGAdminId = table.Column<int>(type: "int", nullable: true),
                    PGtype = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGTypes", x => x.PGTypes_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PGAdminRegisters");

            migrationBuilder.DropTable(
                name: "PGsheringType");

            migrationBuilder.DropTable(
                name: "PGTypes");

            migrationBuilder.DropColumn(
                name: "Created_date",
                table: "RegisterTable");
        }
    }
}
