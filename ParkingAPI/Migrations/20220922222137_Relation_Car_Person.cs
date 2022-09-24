using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingAPI.Migrations
{
    public partial class Relation_Car_Person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isReserved",
                table: "ParkingSpots",
                newName: "IsReserved");

            migrationBuilder.RenameColumn(
                name: "isOccupied",
                table: "ParkingSpots",
                newName: "IsOccupied");

            migrationBuilder.AddColumn<bool>(
                name: "IsParked",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PersonId",
                table: "Cars",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Persons_PersonId",
                table: "Cars",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Persons_PersonId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_PersonId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsParked",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "IsReserved",
                table: "ParkingSpots",
                newName: "isReserved");

            migrationBuilder.RenameColumn(
                name: "IsOccupied",
                table: "ParkingSpots",
                newName: "isOccupied");
        }
    }
}
