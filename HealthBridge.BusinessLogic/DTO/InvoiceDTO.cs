using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.DTO
{
    public class InvoiceDTO
    {
        public InvoiceDTO()
        {
            patient = new PatientDTO();
        }
        public long InvoiceId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMM yyyy HH:mm}")]
        public DateTime InvoiceDateTime { get; set; }
        public long PatientId { get; set; }
        public decimal InvoiceTotal { get; set; }
        public PatientDTO patient { get; set; }
    }
}
