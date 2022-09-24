using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingAPI.Migrations
{
    public partial class CarAndParkingSpotForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "ParkingSpots");

            migrationBuilder.AddColumn<int>(
                name: "CarSpotId",
                table: "ParkingSpots",
                type: "int",
                nullable: true);

            migrationBuilder.DropForeignKey(
                name: "FK_CarSpots_Cars_CarId",
                table: "CarSpots");

            migrationBuilder.DropForeignKey(
                name: "FK_CarSpots_ParkingSpots_SpotId",
                table: "CarSpots");

            migrationBuilder.DropIndex(
                name: "IX_CarSpots_CarId",
                table: "CarSpots");

            migrationBuilder.DropIndex(
                name: "IX_CarSpots_SpotId",
                table: "CarSpots");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_CarSpotId",
                table: "ParkingSpots",
                column: "CarSpotId",
                unique: true,
                filter: "[CarSpotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarSpotId",
                table: "Cars",
                column: "CarSpotId",
                unique: true,
                filter: "[CarSpotId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarSpots_CarSpotId",
                table: "Cars",
                column: "CarSpotId",
                principalTable: "CarSpots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_CarSpots_CarSpotId",
                table: "ParkingSpots",
                column: "CarSpotId",
                principalTable: "CarSpots",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarSpots_CarSpotId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_CarSpots_CarSpotId",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_CarSpotId",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarSpotId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarSpotId",
                table: "ParkingSpots");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "ParkingSpots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarSpots_CarId",
                table: "CarSpots",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarSpots_SpotId",
                table: "CarSpots",
                column: "SpotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarSpots_Cars_CarId",
                table: "CarSpots",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarSpots_ParkingSpots_SpotId",
                table: "CarSpots",
                column: "SpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
