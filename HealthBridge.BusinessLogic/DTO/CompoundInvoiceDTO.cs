using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.DTO
{
    public class CompoundInvoiceDTO
    {
        public CompoundInvoiceDTO()
        {
            InvoiceDetails = new InvoiceToSaveDTO();
            InvoiceLineItems = new List<InvoiceLineDTO>();
        }
        public InvoiceToSaveDTO InvoiceDetails { get; set; }
        public List<InvoiceLineDTO> InvoiceLineItems { get; set; }
    }
}
