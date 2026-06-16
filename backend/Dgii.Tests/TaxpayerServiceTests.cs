using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dgii.Application.Interfaces;
using Dgii.Application.Services;
using Dgii.Domain.Entities;
using Moq;
using Xunit;

namespace Dgii.Tests
{
    public class TaxpayerServiceTests
    {
        [Fact]
        public async Task GetTaxpayerDetailsAsync_CalculatesTotalItbisCorrectly()
        {
            var taxpayerRepoMock = new Mock<ITaxpayerRepository>();
            var taxReceiptRepoMock = new Mock<ITaxReceiptRepository>();

            var rncCedula = "98754321012";
            var taxpayer = new Taxpayer { RncCedula = rncCedula, Nombre = "JUAN PEREZ", Tipo = "PERSONA FISICA", Estatus = "activo" };
            
            var receipts = new List<TaxReceipt>
            {
                new TaxReceipt { RncCedula = rncCedula, NCF = "E310000000001", Monto = 200.00m, Itbis18 = 36.00m },
                new TaxReceipt { RncCedula = rncCedula, NCF = "E310000000002", Monto = 1000.00m, Itbis18 = 180.00m }
            };

            taxpayerRepoMock.Setup(repo => repo.GetByIdAsync(rncCedula)).ReturnsAsync(taxpayer);
            taxReceiptRepoMock.Setup(repo => repo.GetByTaxpayerIdAsync(rncCedula)).ReturnsAsync(receipts);

            var service = new TaxpayerService(taxpayerRepoMock.Object, taxReceiptRepoMock.Object);

            var result = await service.GetTaxpayerDetailsAsync(rncCedula);

            Assert.NotNull(result);
            Assert.Equal(rncCedula, result.RncCedula);
            Assert.Equal(2, result.Receipts.Count());
            Assert.Equal(216.00m, result.TotalItbis);
        }

        [Fact]
        public async Task GetTaxpayerDetailsAsync_ReturnsNull_WhenTaxpayerNotFound()
        {
            var taxpayerRepoMock = new Mock<ITaxpayerRepository>();
            var taxReceiptRepoMock = new Mock<ITaxReceiptRepository>();

            taxpayerRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Taxpayer?)null);

            var service = new TaxpayerService(taxpayerRepoMock.Object, taxReceiptRepoMock.Object);

            var result = await service.GetTaxpayerDetailsAsync("00000");

            Assert.Null(result);
        }
    }
}
