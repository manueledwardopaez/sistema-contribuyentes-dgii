using System.Collections.Generic;

namespace Dgii.Domain.Entities
{
    public class Taxpayer
    {
        public string RncCedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Estatus { get; set; } = string.Empty;

        public ICollection<TaxReceipt> TaxReceipts { get; set; } = new List<TaxReceipt>();
    }
}
