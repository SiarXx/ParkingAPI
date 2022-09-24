using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingAPI.Migrations
{
    public partial class RemovalOfCarSpotTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarSpots_CarSpotId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_CarSpots_CarSpotId",
                table: "ParkingSpots");

            migrationBuilder.DropTable(
                name: "CarSpots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSpots_CarSpotId",
                table: "ParkingSpots");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarSpotId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "CarSpotId",
                table: "ParkingSpots",
                newName: "CarId");

            migrationBuilder.RenameColumn(
                name: "CarSpotId",
                table: "Cars",
                newName: "ParkedSpotId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ParkingSpots_ParkedSpotId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ParkedSpotId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "ParkingSpots",
                newName: "CarSpotId");

            migrationBuilder.RenameColumn(
                name: "ParkedSpotId",
                table: "Cars",
                newName: "CarSpotId");

            migrationBuilder.CreateTable(
                name: "CarSpots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    SpotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarSpots", x => x.Id);
                });

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
    }
}
