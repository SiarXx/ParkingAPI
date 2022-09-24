using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingAPI.Migrations
{
    public partial class CarSpotIntermediateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "ParkingSpots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarSpotId",
                table: "Cars",
                type: "int",
                nullable: true);

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
                    table.ForeignKey(
                        name: "FK_CarSpots_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarSpots_ParkingSpots_SpotId",
                        column: x => x.SpotId,
                        principalTable: "ParkingSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarSpots");

            migrationBuilder.DropColumn(
                name: "CarSpotId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarSpotId",
                table: "ParkingSpots");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpots_CarId",
                table: "ParkingSpots",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_Cars_CarId",
                table: "ParkingSpots",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }
    }
}
