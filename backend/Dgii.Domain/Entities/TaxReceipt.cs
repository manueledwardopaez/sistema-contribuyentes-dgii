namespace Dgii.Domain.Entities
{
    public class TaxReceipt
    {
        public string NCF { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public decimal Itbis18 { get; set; }

        public string RncCedula { get; set; } = string.Empty;
        
        public Taxpayer? Taxpayer { get; set; }
    }
}
