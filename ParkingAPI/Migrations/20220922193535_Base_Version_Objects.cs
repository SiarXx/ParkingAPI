using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingAPI.Migrations
{
    public partial class Base_Version_Objects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot");

            migrationBuilder.RenameTable(
                name: "ParkingSpot",
                newName: "ParkingSpots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Registration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSpots",
                table: "ParkingSpots");

            migrationBuilder.RenameTable(
                name: "ParkingSpots",
                newName: "ParkingSpot");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSpot",
                table: "ParkingSpot",
                column: "Id");
        }
    }
}
