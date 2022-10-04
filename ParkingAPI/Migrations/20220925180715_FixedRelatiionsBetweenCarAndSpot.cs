using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingAPI.Migrations
{
    public partial class FixedRelatiionsBetweenCarAndSpot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ParkingSpots_ParkedSpotId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Persons_PersonId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ParkedSpotId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsParked",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ParkedSpotId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "ParkingSpots",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_CarId",
                table: "ParkingSpots",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_OwnerId",
                table: "ParkingSpots",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Persons_PersonId",
                table: "Cars",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_Cars_CarId",
                table: "ParkingSpots",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_Persons_OwnerId",
                table: "ParkingSpots",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Persons_PersonId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_Cars_CarId",
                table: "ParkingSpots");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_Persons_OwnerId",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_CarId",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_OwnerId",
                table: "ParkingSpots");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ParkingSpots");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsParked",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParkedSpotId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ParkedSpotId",
                table: "Cars",
                column: "ParkedSpotId",
                unique: true,
                filter: "[ParkedSpotId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ParkingSpots_ParkedSpotId",
                table: "Cars",
                column: "ParkedSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Persons_PersonId",
                table: "Cars",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
