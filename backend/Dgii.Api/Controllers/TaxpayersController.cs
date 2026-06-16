using System.Collections.Generic;
using System.Threading.Tasks;
using Dgii.Application.DTOs;
using Dgii.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dgii.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json", "application/xml")]
    public class TaxpayersController : ControllerBase
    {
        private readonly ITaxpayerService _taxpayerService;

        public TaxpayersController(ITaxpayerService taxpayerService)
        {
            _taxpayerService = taxpayerService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TaxpayerDto>>> GetPagedTaxpayers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagedResult = await _taxpayerService.GetPagedTaxpayersAsync(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("receipts")]
        public async Task<ActionResult<IEnumerable<TaxReceiptDto>>> GetAllTaxReceipts()
        {
            var receipts = await _taxpayerService.GetAllTaxReceiptsAsync();
            return Ok(receipts);
        }

        [HttpGet("{rncCedula}")]
        public async Task<ActionResult<TaxpayerDetailsDto>> GetTaxpayerDetails(string rncCedula)
        {
            var taxpayerDetails = await _taxpayerService.GetTaxpayerDetailsAsync(rncCedula);
            
            if (taxpayerDetails == null)
            {
                return NotFound($"Taxpayer with RNC/Cédula {rncCedula} not found.");
            }

            return Ok(taxpayerDetails);
        }
    }
}
