using HealthBridge.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.Interfaces
{
    public interface IPatient
    {
        Task<PatientDTO> GetIndividualPatient(long patientId);
        Task<List<PatientDTO>> GetAllPatients();
        Task<int> SavePatient(PatientDTO newPatient);
        Task<int> DeletePatient(int patientId);
        Task<int> UpdatePatient(PatientDTO currentPatient);
        Task<List<PatientDropDownDTO>> PatientDropDownDisplay();
    }
}
