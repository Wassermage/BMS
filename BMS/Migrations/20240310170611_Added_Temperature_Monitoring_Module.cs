using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Migrations
{
    public partial class Added_Temperature_Monitoring_Module : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureReaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureReaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureReaders_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureReadouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemperatureReaderId = table.Column<int>(type: "int", nullable: false),
                    TemperatureC = table.Column<int>(type: "int", nullable: false),
                    readoutTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureReadouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureReadouts_TemperatureReaders_TemperatureReaderId",
                        column: x => x.TemperatureReaderId,
                        principalTable: "TemperatureReaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureReaders_RoomId",
                table: "TemperatureReaders",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureReadouts_TemperatureReaderId",
                table: "TemperatureReadouts",
                column: "TemperatureReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemperatureReadouts");

            migrationBuilder.DropTable(
                name: "TemperatureReaders");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
