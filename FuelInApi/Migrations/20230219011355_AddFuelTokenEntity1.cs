using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelInApi.Migrations
{
    public partial class AddFuelTokenEntity1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelTokenRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledFillingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TolerenceUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentDoneOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FuelCollectedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestedQuotaInLitres = table.Column<int>(type: "int", nullable: false),
                    RequestedFuelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelStationId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    FuelOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTokenRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelTokenRequests_FuelOrders_FuelOrderId",
                        column: x => x.FuelOrderId,
                        principalTable: "FuelOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuelTokenRequests_FuelStations_FuelStationId",
                        column: x => x.FuelStationId,
                        principalTable: "FuelStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FuelTokenRequests_SystemUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelTokenRequests_DriverId",
                table: "FuelTokenRequests",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelTokenRequests_FuelOrderId",
                table: "FuelTokenRequests",
                column: "FuelOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelTokenRequests_FuelStationId",
                table: "FuelTokenRequests",
                column: "FuelStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelTokenRequests");
        }
    }
}
