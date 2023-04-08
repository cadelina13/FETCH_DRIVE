using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fetch.App.Data.Migrations
{
    public partial class initialtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickUpId = table.Column<int>(type: "int", nullable: true),
                    DropOffId = table.Column<int>(type: "int", nullable: true),
                    PickUpSchedule = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Locations_DropOffId",
                        column: x => x.DropOffId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Locations_PickUpId",
                        column: x => x.PickUpId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DropOffId",
                table: "Bookings",
                column: "DropOffId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PickUpId",
                table: "Bookings",
                column: "PickUpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
