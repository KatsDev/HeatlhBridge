using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.DTO
{
    public class InvoiceLineDTO
    {
        public long InvoiceLineId { get; set; }
        public long InvoiceId { get; set; }
        public float Qty { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal LineTotal { get; set; }
    }
}
