using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addition_of_transportation_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Received_Amount",
                table: "Transportations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Received_Currency",
                table: "Transportations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    ToId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    Expendature_Amount = table.Column<double>(type: "float", nullable: false),
                    Expendature_Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportationId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Costs_Stops_FromId",
                        column: x => x.FromId,
                        principalTable: "Stops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Costs_Stops_ToId",
                        column: x => x.ToId,
                        principalTable: "Stops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Costs_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalTable: "Transportations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Costs_FromId",
                table: "Costs",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ToId",
                table: "Costs",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_TransportationId",
                table: "Costs",
                column: "TransportationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropColumn(
                name: "Received_Amount",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "Received_Currency",
                table: "Transportations");
        }
    }
}
