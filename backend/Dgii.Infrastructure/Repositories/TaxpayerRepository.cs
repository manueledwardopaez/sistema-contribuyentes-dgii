using System.Collections.Generic;
using System.Threading.Tasks;
using Dgii.Application.Interfaces;
using Dgii.Domain.Entities;
using Dgii.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dgii.Infrastructure.Repositories
{
    public class TaxpayerRepository : ITaxpayerRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxpayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Taxpayer>> GetAllAsync()
        {
            return await _context.Taxpayers.ToListAsync();
        }

        public async Task<(IEnumerable<Taxpayer> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Taxpayers.CountAsync();
            var items = await _context.Taxpayers
                .OrderBy(t => t.RncCedula)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Taxpayer?> GetByIdAsync(string rncCedula)
        {
            return await _context.Taxpayers.FirstOrDefaultAsync(t => t.RncCedula == rncCedula);
        }
    }
}
