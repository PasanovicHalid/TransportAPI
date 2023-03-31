using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_of_connection_of_trailer_to_company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stop_Transportations_TransportationId",
                table: "Stop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stop",
                table: "Stop");

            migrationBuilder.RenameTable(
                name: "Stop",
                newName: "Stops");

            migrationBuilder.RenameIndex(
                name: "IX_Stop_TransportationId",
                table: "Stops",
                newName: "IX_Stops_TransportationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stops",
                table: "Stops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Transportations_TransportationId",
                table: "Stops",
                column: "TransportationId",
                principalTable: "Transportations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Transportations_TransportationId",
                table: "Stops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stops",
                table: "Stops");

            migrationBuilder.RenameTable(
                name: "Stops",
                newName: "Stop");

            migrationBuilder.RenameIndex(
                name: "IX_Stops_TransportationId",
                table: "Stop",
                newName: "IX_Stop_TransportationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stop",
                table: "Stop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stop_Transportations_TransportationId",
                table: "Stop",
                column: "TransportationId",
                principalTable: "Transportations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
