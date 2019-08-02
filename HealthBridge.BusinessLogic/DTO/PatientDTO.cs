using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.DTO
{
    public class PatientDTO
    {
        public long PatientId { get; set; }
        [Required(ErrorMessage ="Please Enter First Name")]
        [DisplayName("First Name")]
        [MaxLength(50, ErrorMessage = "First Name cannot be greater than 50")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        [DisplayName("Last Name")]
        [MaxLength(50, ErrorMessage = "Last Name cannot be greater than 50")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter ID Number")]
        [DisplayName("ID Number")]
        [MaxLength(13, ErrorMessage = "ID Number cannot be greater than 13")]
        public string IdNumber { get; set; }
        public int PatientCount { get; set; }
    }
}
