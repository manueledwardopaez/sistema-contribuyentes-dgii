using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dgii.Application.Interfaces;
using Dgii.Domain.Entities;
using Dgii.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dgii.Infrastructure.Repositories
{
    public class TaxReceiptRepository : ITaxReceiptRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxReceipt>> GetAllAsync()
        {
            return await _context.TaxReceipts.ToListAsync();
        }

        public async Task<IEnumerable<TaxReceipt>> GetByTaxpayerIdAsync(string rncCedula)
        {
            return await _context.TaxReceipts
                .Where(tr => tr.RncCedula == rncCedula)
                .ToListAsync();
        }
    }
}
