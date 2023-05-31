using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HL.DAL.Migrations
{
    public partial class HL1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTP",
                table: "RegisterTable");

            migrationBuilder.CreateTable(
                name: "OTPClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPClass", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTPClass");

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "RegisterTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
