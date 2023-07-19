using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modification_to_transportation_model_addition_of_start_location : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StartLocation_Latitude",
                table: "Transportations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StartLocation_Longitude",
                table: "Transportations",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartLocation_Latitude",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "StartLocation_Longitude",
                table: "Transportations");
        }
    }
}
