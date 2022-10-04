using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingAPI.Migrations
{
    public partial class ParkingSpotRemovalOfOwnerReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_Persons_OwnerId",
                table: "ParkingSpots");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "ParkingSpots",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingSpots_OwnerId",
                table: "ParkingSpots",
                newName: "IX_ParkingSpots_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_Persons_PersonId",
                table: "ParkingSpots",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_Persons_PersonId",
                table: "ParkingSpots");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "ParkingSpots",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingSpots_PersonId",
                table: "ParkingSpots",
                newName: "IX_ParkingSpots_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_Persons_OwnerId",
                table: "ParkingSpots",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
