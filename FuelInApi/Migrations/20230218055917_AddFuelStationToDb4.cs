using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelInApi.Migrations
{
    public partial class AddFuelStationToDb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerId",
                table: "FuelStations");

            migrationBuilder.DropIndex(
                name: "IX_FuelStations_ManagerId",
                table: "FuelStations");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "FuelStations");

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_ManagerUserId",
                table: "FuelStations",
                column: "ManagerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerUserId",
                table: "FuelStations",
                column: "ManagerUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerUserId",
                table: "FuelStations");

            migrationBuilder.DropIndex(
                name: "IX_FuelStations_ManagerUserId",
                table: "FuelStations");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "FuelStations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FuelStations_ManagerId",
                table: "FuelStations",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerId",
                table: "FuelStations",
                column: "ManagerId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
