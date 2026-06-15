using System.Collections.Generic;

namespace Dgii.Application.DTOs
{
    public class TaxpayerDetailsDto : TaxpayerDto
    {
        public decimal TotalItbis { get; set; }
        public IEnumerable<TaxReceiptDto> Receipts { get; set; } = new List<TaxReceiptDto>();
    }
}
