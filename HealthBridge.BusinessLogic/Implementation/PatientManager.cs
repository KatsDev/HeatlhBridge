using HealthBridge.BusinessLogic.DTO;
using HealthBridge.BusinessLogic.Interfaces;
using HealthBridge.DataAccess;
using HealthBridge.DataAccess.Implementation;
using HealthBridge.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.Implementation
{
    public class PatientManager : IPatient
    {
        private IGenericRepository<Patient> _patientRepository = null;

        public PatientManager()
        {
            this._patientRepository = new GenericRepository<Patient>();
        }

        public async Task<List<PatientDTO>> GetAllPatients()
        {
            try
            {
                var patientList = new List<PatientDTO>();

                var patientsInDB = await _patientRepository.GetAll();

                foreach(var patient in patientsInDB)
                {
                    PatientDTO patientDTO = new PatientDTO();
                    patientDTO.PatientId = patient.PatientId;
                    patientDTO.FirstName = patient.FirstName;
                    patientDTO.LastName = patient.LastName;
                    patientDTO.IdNumber = patient.IdNumber;
                    patientList.Add(patientDTO);
                }

                return patientList.OrderBy(x => x.FirstName).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PatientDropDownDTO>> PatientDropDownDisplay()
        {
            try
            {
                var patientList = new List<PatientDropDownDTO>();

                var patientsInDB = await _patientRepository.GetAll();

                foreach(var patient in patientsInDB)
                {
                    PatientDropDownDTO patientDropDown = new PatientDropDownDTO();
                    patientDropDown.PatientId = patient.PatientId;
                    patientDropDown.DisplayName = patient.FirstName + ' ' + patient.LastName;
                    patientList.Add(patientDropDown);
                }

                return patientList.OrderBy(x => x.DisplayName).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SavePatient(PatientDTO newPatient)
        {
            try
            {
                Patient patient = new Patient();

                patient.FirstName = newPatient.FirstName;
                patient.LastName = newPatient.LastName;
                patient.IdNumber = newPatient.IdNumber;

                _patientRepository.Insert(patient);

                var result = await _patientRepository.Save();

                newPatient.PatientId = patient.PatientId;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeletePatient(int patientId)
        {
            try
            {
                _patientRepository.Delete(patientId);

                var result = await _patientRepository.Save();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdatePatient(PatientDTO currentPatient)
        {
            try
            {
                Patient patient = new Patient();

                patient = await _patientRepository.GetById(currentPatient.PatientId);

                patient.FirstName = currentPatient.FirstName;
                patient.LastName = currentPatient.LastName;
                patient.IdNumber = currentPatient.IdNumber;

                _patientRepository.Update(patient);

                var result = await _patientRepository.Save();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PatientDTO> GetIndividualPatient(long patientId)
        {
            try
            {
                PatientDTO patient = new PatientDTO();

                var patientInDb = await _patientRepository.GetById(patientId);

                if(patientInDb != null)
                {
                    patient.PatientId = patientInDb.PatientId;
                    patient.FirstName = patientInDb.FirstName;
                    patient.LastName = patientInDb.LastName;
                    patient.IdNumber = patientInDb.IdNumber;
                    return patient;
                }
                else
                {
                    patient = null;
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
