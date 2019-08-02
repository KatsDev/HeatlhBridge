using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.DTO
{
    public class InvoiceUpdateDTO
    {
        public InvoiceToSaveDTO InvoiceDetails { get; set; }
        public InvoiceLineDTO InvoiceLineItem { get; set; }
    }
}
