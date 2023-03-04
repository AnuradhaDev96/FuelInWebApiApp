using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelInApi.Migrations
{
    public partial class AddFuelOrderToDb6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelOrders_FuelStations_FuelStationId",
                table: "FuelOrders");

            migrationBuilder.AlterColumn<int>(
                name: "FuelStationId",
                table: "FuelOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FuelOrders_FuelStations_FuelStationId",
                table: "FuelOrders",
                column: "FuelStationId",
                principalTable: "FuelStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelOrders_FuelStations_FuelStationId",
                table: "FuelOrders");

            migrationBuilder.AlterColumn<int>(
                name: "FuelStationId",
                table: "FuelOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FuelOrders_FuelStations_FuelStationId",
                table: "FuelOrders",
                column: "FuelStationId",
                principalTable: "FuelStations",
                principalColumn: "Id");
        }
    }
}
