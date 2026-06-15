using System.Collections.Generic;
using System.Threading.Tasks;
using Dgii.Domain.Entities;

namespace Dgii.Application.Interfaces
{
    public interface ITaxReceiptRepository
    {
        Task<IEnumerable<TaxReceipt>> GetAllAsync();
        Task<IEnumerable<TaxReceipt>> GetByTaxpayerIdAsync(string rncCedula);
    }
}
