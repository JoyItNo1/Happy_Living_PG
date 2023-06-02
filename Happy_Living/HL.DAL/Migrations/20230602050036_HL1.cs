using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HL.DAL.Migrations
{
    public partial class HL1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Usertype",
                table: "UserTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "building_blockclas",
                columns: table => new
                {
                    building_blockclas_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    blockclas_No = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_building_blockclas", x => x.building_blockclas_Id);
                });

            migrationBuilder.CreateTable(
                name: "Flore",
                columns: table => new
                {
                    Flore_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    block_no = table.Column<int>(type: "int", nullable: true),
                    Flore_No = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flore", x => x.Flore_Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageTable",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGAdminId = table.Column<int>(type: "int", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentsImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PGUserTable",
                columns: table => new
                {
                    PGUser_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGAdminId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bulding_no = table.Column<int>(type: "int", nullable: true),
                    Flour_no = table.Column<int>(type: "int", nullable: true),
                    Room_no = table.Column<int>(type: "int", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HashPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role_Id = table.Column<int>(type: "int", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGUserTable", x => x.PGUser_Id);
                });

            migrationBuilder.CreateTable(
                name: "PGWorks",
                columns: table => new
                {
                    PGWorks_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGAdminId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PGWorks", x => x.PGWorks_Id);
                });

            migrationBuilder.CreateTable(
                name: "Room_Sharing",
                columns: table => new
                {
                    Room_Sharing_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rooms_No = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    room_sharing = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room_Sharing", x => x.Room_Sharing_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Rooms_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Flore_No = table.Column<int>(type: "int", nullable: true),
                    Room_no = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Rooms_Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedPGUser",
                columns: table => new
                {
                    SelectedPGUser_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGAdminId = table.Column<int>(type: "int", nullable: true),
                    Select_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select_Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select_PG_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select_PG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sharing_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sharing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedPGUser", x => x.SelectedPGUser_Id);
                });

            migrationBuilder.CreateTable(
                name: "Stetus",
                columns: table => new
                {
                    Stetus_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGUser_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bulding_no = table.Column<int>(type: "int", nullable: true),
                    Flour_no = table.Column<int>(type: "int", nullable: true),
                    Room_no = table.Column<int>(type: "int", nullable: true),
                    Stetuss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stetus", x => x.Stetus_Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSuggetionCmplet",
                columns: table => new
                {
                    SC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    User_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuggetionOrCmplet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Block_no = table.Column<int>(type: "int", nullable: true),
                    Flour_no = table.Column<int>(type: "int", nullable: true),
                    Room_no = table.Column<int>(type: "int", nullable: true),
                    Created_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSuggetionCmplet", x => x.SC_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "building_blockclas");

            migrationBuilder.DropTable(
                name: "Flore");

            migrationBuilder.DropTable(
                name: "ImageTable");

            migrationBuilder.DropTable(
                name: "PaymentImage");

            migrationBuilder.DropTable(
                name: "PGUserTable");

            migrationBuilder.DropTable(
                name: "PGWorks");

            migrationBuilder.DropTable(
                name: "Room_Sharing");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "SelectedPGUser");

            migrationBuilder.DropTable(
                name: "Stetus");

            migrationBuilder.DropTable(
                name: "UserSuggetionCmplet");

            migrationBuilder.AlterColumn<string>(
                name: "Usertype",
                table: "UserTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
