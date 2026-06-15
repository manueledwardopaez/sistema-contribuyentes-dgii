using System.Collections.Generic;
using System.Threading.Tasks;
using Dgii.Application.DTOs;

namespace Dgii.Application.Services
{
    public interface ITaxpayerService
    {
        Task<IEnumerable<TaxpayerDto>> GetAllTaxpayersAsync();
        Task<PagedResult<TaxpayerDto>> GetPagedTaxpayersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<TaxReceiptDto>> GetAllTaxReceiptsAsync();
        Task<TaxpayerDetailsDto?> GetTaxpayerDetailsAsync(string rncCedula);
    }
}
