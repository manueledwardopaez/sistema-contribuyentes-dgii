using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dgii.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataActivosYComprobantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaxReceipts",
                columns: new[] { "NCF", "Itbis18", "Monto", "RncCedula" },
                values: new object[,]
                {
                    { "E310000000010", 90.00m, 500.00m, "98754321012" },
                    { "E310000000011", 54.00m, 300.00m, "123456789" },
                    { "E310000000012", 81.00m, 450.00m, "123456789" },
                    { "E310000000013", 21.60m, 120.00m, "123456789" },
                    { "E310000000014", 414.00m, 2300.00m, "101010101" },
                    { "E310000000015", 144.00m, 800.00m, "202020202" },
                    { "E310000000016", 198.00m, 1100.00m, "202020202" },
                    { "E310000000017", 720.00m, 4000.00m, "303030303" },
                    { "E310000000018", 1170.00m, 6500.00m, "303030303" },
                    { "E310000000019", 225.00m, 1250.00m, "303030303" },
                    { "E310000000020", 108.00m, 600.00m, "404040404" },
                    { "E310000000021", 63.00m, 350.00m, "404040404" },
                    { "E310000000022", 450.00m, 2500.00m, "505050505" },
                    { "E310000000023", 324.00m, 1800.00m, "505050505" },
                    { "E310000000024", 171.00m, 950.00m, "505050505" },
                    { "E310000000025", 135.00m, 750.00m, "606060606" },
                    { "E310000000026", 234.00m, 1300.00m, "606060606" },
                    { "E310000000027", 1044.00m, 5800.00m, "707070707" },
                    { "E310000000028", 27.00m, 150.00m, "808080808" },
                    { "E310000000029", 75.60m, 420.00m, "808080808" },
                    { "E310000000030", 176.40m, 980.00m, "808080808" }
                });

            migrationBuilder.UpdateData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "123456789",
                column: "Estatus",
                value: "activo");

            migrationBuilder.UpdateData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "505050505",
                column: "Estatus",
                value: "activo");

            migrationBuilder.UpdateData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "808080808",
                column: "Estatus",
                value: "activo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000010");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000011");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000012");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000013");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000014");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000015");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000016");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000017");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000018");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000019");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000020");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000021");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000022");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000023");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000024");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000025");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000026");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000027");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000028");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000029");

            migrationBuilder.DeleteData(
                table: "TaxReceipts",
                keyColumn: "NCF",
                keyValue: "E310000000030");

            migrationBuilder.UpdateData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "123456789",
                column: "Estatus",
                value: "inactivo");

            migrationBuilder.UpdateData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "505050505",
                column: "Estatus",
                value: "inactivo");

            migrationBuilder.UpdateData(
                table: "Taxpayers",
                keyColumn: "RncCedula",
                keyValue: "808080808",
                column: "Estatus",
                value: "inactivo");
        }
    }
}
