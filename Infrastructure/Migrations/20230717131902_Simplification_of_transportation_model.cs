using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Simplification_of_transportation_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Transportations_TransportationId",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_TransportationId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "TransportationId",
                table: "Costs");

            migrationBuilder.AddColumn<double>(
                name: "Cost_Amount",
                table: "Transportations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Cost_Currency",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination_City",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination_Country",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Destination_GpsCoordinate_Latitude",
                table: "Transportations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Destination_GpsCoordinate_Longitude",
                table: "Transportations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Destination_PostalCode",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination_State",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Destination_Street",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost_Amount",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Cost_Currency",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Destination_City",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Destination_Country",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Destination_GpsCoordinate_Latitude",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Destination_GpsCoordinate_Longitude",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Destination_PostalCode",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Destination_State",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Destination_Street",
                table: "Transportations");

            migrationBuilder.AddColumn<decimal>(
                name: "TransportationId",
                table: "Costs",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_TransportationId",
                table: "Costs",
                column: "TransportationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Transportations_TransportationId",
                table: "Costs",
                column: "TransportationId",
                principalTable: "Transportations",
                principalColumn: "Id");
        }
    }
}
