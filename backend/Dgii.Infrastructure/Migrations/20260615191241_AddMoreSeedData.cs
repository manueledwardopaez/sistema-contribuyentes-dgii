using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dgii.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Taxpayers",
                columns: new[] { "RncCedula", "Estatus", "Nombre", "Tipo" },
                values: new object[,]
                {
                    { "101010101", "activo", "SUPERMERCADOS NACIONAL", "PERSONA JURIDICA" },
                    { "202020202", "activo", "CLARO DOMINICANA", "PERSONA JURIDICA" },
                    { "303030303", "activo", "BANCO POPULAR DOMINICANO", "PERSONA JURIDICA" },
                    { "404040404", "activo", "MARIA GOMEZ", "PERSONA FISICA" },
                    { "505050505", "inactivo", "FERRETERIA AMERICANA", "PERSONA JURIDICA" },
                    { "606060606", "activo", "JOSE RODRIGUEZ", "PERSONA FISICA" },
                    { "707070707", "activo", "GRUPO RAMOS", "PERSONA JURIDICA" },
                    { "808080808", "inactivo", "ANA MARTINEZ", "PERSONA FISICA" }
                });

            migrationBuilder.InsertData(
                table: "TaxReceipts",
                columns: new[] { "NCF", "Itbis18", "Monto", "RncCedula" },
                values: new object[,]
                {
                    { "E310000000003", 900.00m, 5000.00m, "101010101" },
                    { "E310000000004", 270.00m, 1500.00m, "101010101" },
                    { "E310000000005", 63.00m, 350.00m, "202020202" },
                    { "E310000000006", 216.00m, 1200.00m, "404040404" },
                    { "E310000000007", 72.00m, 400.00m, "606060606" },
                    { "E310000000008", 1800.00m, 10000.00m, "707070707" },
                    { "E310000000009", 450.00m, 2500.00m, "707070707" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000003");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000004");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000005");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000006");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000007");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000008");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000009");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "303030303");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "505050505");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "808080808");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "101010101");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "202020202");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "404040404");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "606060606");

            migrationBuilder.DeleteData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "707070707");
        }
    }
}
