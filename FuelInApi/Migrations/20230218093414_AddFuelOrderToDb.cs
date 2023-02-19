using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelInApi.Migrations
{
    public partial class AddFuelOrderToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderQuantityInLitres = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelStationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelOrders_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelOrders_FuelStationId",
                table: "FuelOrders",
                column: "FuelStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelOrders");
        }
    }
}
