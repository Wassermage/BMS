using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMS.Migrations
{
    /// <inheritdoc />
    public partial class Updated_TemperatureReadout_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "readoutTime",
                table: "TemperatureReadouts",
                newName: "ReadoutTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReadoutTime",
                table: "TemperatureReadouts",
                newName: "readoutTime");
        }
    }
}
