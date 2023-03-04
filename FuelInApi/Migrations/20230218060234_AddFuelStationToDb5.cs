using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelInApi.Migrations
{
    public partial class AddFuelStationToDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerUserId",
                table: "FuelStations");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerUserId",
                table: "FuelStations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerUserId",
                table: "FuelStations",
                column: "ManagerUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerUserId",
                table: "FuelStations");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerUserId",
                table: "FuelStations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FuelStations_SystemUsers_ManagerUserId",
                table: "FuelStations",
                column: "ManagerUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
