using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Expansions_of_transportation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Origin_City",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_Country",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Origin_GpsCoordinate_Latitude",
                table: "Transportations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Origin_GpsCoordinate_Longitude",
                table: "Transportations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin_PostalCode",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_State",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Origin_Street",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Origin_City",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Origin_Country",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Origin_GpsCoordinate_Latitude",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Origin_GpsCoordinate_Longitude",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Origin_PostalCode",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Origin_State",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Origin_Street",
                table: "Transportations");
        }
    }
}
