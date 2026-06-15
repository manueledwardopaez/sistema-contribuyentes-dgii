using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dgii.Application.DTOs;
using Dgii.Application.Interfaces;
using Dgii.Domain.Entities;

namespace Dgii.Application.Services
{
    public class TaxpayerService : ITaxpayerService
    {
        private readonly ITaxpayerRepository _taxpayerRepository;
        private readonly ITaxReceiptRepository _taxReceiptRepository;

        public TaxpayerService(ITaxpayerRepository taxpayerRepository, ITaxReceiptRepository taxReceiptRepository)
        {
            _taxpayerRepository = taxpayerRepository;
            _taxReceiptRepository = taxReceiptRepository;
        }

        public async Task<IEnumerable<TaxpayerDto>> GetAllTaxpayersAsync()
        {
            var taxpayers = await _taxpayerRepository.GetAllAsync();
            return taxpayers.Select(t => new TaxpayerDto
            {
                RncCedula = t.RncCedula,
                Nombre = t.Nombre,
                Tipo = t.Tipo,
                Estatus = t.Estatus
            });
        }

        public async Task<PagedResult<TaxpayerDto>> GetPagedTaxpayersAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _taxpayerRepository.GetPagedAsync(pageNumber, pageSize);
            
            var dtos = items.Select(t => new TaxpayerDto
            {
                RncCedula = t.RncCedula,
                Nombre = t.Nombre,
                Tipo = t.Tipo,
                Estatus = t.Estatus
            });

            return new PagedResult<TaxpayerDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<TaxReceiptDto>> GetAllTaxReceiptsAsync()
        {
            var receipts = await _taxReceiptRepository.GetAllAsync();
            return receipts.Select(r => new TaxReceiptDto
            {
                RncCedula = r.RncCedula,
                NCF = r.NCF,
                Monto = r.Monto,
                Itbis18 = r.Itbis18
            });
        }

        public async Task<TaxpayerDetailsDto?> GetTaxpayerDetailsAsync(string rncCedula)
        {
            var taxpayer = await _taxpayerRepository.GetByIdAsync(rncCedula);
            if (taxpayer == null)
            {
                return null;
            }

            var receipts = await _taxReceiptRepository.GetByTaxpayerIdAsync(rncCedula);

            var receiptDtos = receipts.Select(r => new TaxReceiptDto
            {
                RncCedula = r.RncCedula,
                NCF = r.NCF,
                Monto = r.Monto,
                Itbis18 = r.Itbis18
            }).ToList();

            var totalItbis = receiptDtos.Sum(r => r.Itbis18);

            return new TaxpayerDetailsDto
            {
                RncCedula = taxpayer.RncCedula,
                Nombre = taxpayer.Nombre,
                Tipo = taxpayer.Tipo,
                Estatus = taxpayer.Estatus,
                TotalItbis = totalItbis,
                Receipts = receiptDtos
            };
        }
    }
}
