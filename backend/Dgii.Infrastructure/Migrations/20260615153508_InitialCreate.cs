using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dgii.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Taxpayers",
                columns: table => new
                {
                    RncCedula = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxpayers", x => x.RncCedula);
                });

            migrationBuilder.CreateTable(
                name: "TaxReceipts",
                columns: table => new
                {
                    NCF = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Itbis18 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RncCedula = table.Column<string>(type: "nvarchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxReceipts", x => x.NCF);
                    table.ForeignKey(
                        name: "FK_TaxReceipts_Taxpayers_RncCedula",
                        column: x => x.RncCedula,
                        principalTable: "Taxpayers",
                        principalColumn: "RncCedula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Taxpayers",
                columns: new[] { "RncCedula", "Estatus", "Nombre", "Tipo" },
                values: new object[,]
                {
                    { "123456789", "inactivo", "FARMACIA TU SALUD", "PERSONA JURIDICA" },
                    { "98754321012", "activo", "JUAN PEREZ", "PERSONA FISICA" }
                });

            migrationBuilder.InsertData(
                table: "TaxReceipts",
                columns: new[] { "NCF", "Itbis18", "Monto", "RncCedula" },
                values: new object[,]
                {
                    { "E310000000001", 36.00m, 200.00m, "98754321012" },
                    { "E310000000002", 180.00m, 1000.00m, "98754321012" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxReceipts_RncCedula",
                table: "TaxReceipts",
                column: "RncCedula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxReceipts");

            migrationBuilder.DropTable(
                name: "Taxpayers");
        }
    }
}
