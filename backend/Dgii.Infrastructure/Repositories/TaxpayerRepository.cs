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

        public async Task<Taxpayer?> GetByIdAsync(string rncCedula)
        {
            return await _context.Taxpayers.FirstOrDefaultAsync(t => t.RncCedula == rncCedula);
        }
    }
}
