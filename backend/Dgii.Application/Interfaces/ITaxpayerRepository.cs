using System.Collections.Generic;
using System.Threading.Tasks;
using Dgii.Domain.Entities;

namespace Dgii.Application.Interfaces
{
    public interface ITaxpayerRepository
    {
        Task<IEnumerable<Taxpayer>> GetAllAsync();
        Task<(IEnumerable<Taxpayer> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task<Taxpayer?> GetByIdAsync(string rncCedula);
    }
}
