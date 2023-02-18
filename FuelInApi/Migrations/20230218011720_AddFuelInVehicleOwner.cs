using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelInApi.Migrations
{
    public partial class AddFuelInVehicleOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelInVehicleOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelInVehicleOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelInVehicleOwners_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleRegistry",
                columns: table => new
                {
                    PlateNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleClass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRegistry", x => x.PlateNumber);
                });

            migrationBuilder.CreateTable(
                name: "FuelInVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelInVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelInVehicles_FuelInVehicleOwners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "FuelInVehicleOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelInVehicleOwners_SystemUserId",
                table: "FuelInVehicleOwners",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelInVehicles_OwnerId",
                table: "FuelInVehicles",
                column: "OwnerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelInVehicles");

            migrationBuilder.DropTable(
                name: "VehicleRegistry");

            migrationBuilder.DropTable(
                name: "FuelInVehicleOwners");
        }
    }
}
