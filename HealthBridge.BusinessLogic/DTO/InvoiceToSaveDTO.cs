using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.DTO
{
    public class InvoiceToSaveDTO
    {

        public InvoiceToSaveDTO()
        {
            InvoiceDateTime = DateTime.Now;
        }
        public long InvoiceId { get; set; }

        //[Required(ErrorMessage = "Please Enter Invoice Date")]
        //[DisplayName("First Name")]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMM yyyy HH:mm}")]
        public DateTime InvoiceDateTime { get; set; }

        [Required(ErrorMessage = "Please Select A Patient")]
        [DisplayName("First Name")]
        public long PatientId { get; set; }

        public decimal InvoiceTotal { get; set; }
    }
}
