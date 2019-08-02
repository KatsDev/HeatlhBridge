using HealthBridge.BusinessLogic.DTO;
using HealthBridge.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HealthBridge.Web.Controllers.Api
{
    // Thin Controller, Fat Service Manager Approach
    [RoutePrefix("api/Patients")]
    public class PatientsController : ApiController
    {
        private IPatient _patient;
        private IInvoice _invoice;

        public PatientsController(IPatient patient, IInvoice invoice)
        {
            _patient = patient;
            _invoice = invoice;
        }

        [HttpGet]
        [Route("getindividualpatient/{patientId}")]
        public async Task<IHttpActionResult> GetIndividualPatient(long patientId)
        {
            try
            {
                var patient = await _patient.GetIndividualPatient(patientId);
                
                if (patient == null)
                    return NotFound();

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getpatientsdropdowndisplay")]
        public async Task<IHttpActionResult> GetPatientsDropDownDisplay()
        {
            try
            {
                var patients = await _patient.PatientDropDownDisplay();

                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getallpatients")]
        public async Task<IHttpActionResult> GetAllPatients()
        {
            try
            {
                var patients = await _patient.GetAllPatients();

                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("savepatient")]
        public async Task<IHttpActionResult> SavePatient(PatientDTO newPatient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _patient.SavePatient(newPatient);

                    if (result > 0)
                        return Ok("New Patient Record Added To Database!");

                    return BadRequest("An Error Has Occurred");
                }
                else
                {
                    return BadRequest("An Error Has Occurred");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletepatient/{patientId}")]
        public async Task<IHttpActionResult> DeletePatient(int patientId)
        {
            try
            {
                var patienInvoiceCount = await _invoice.patientInvoiceExists(patientId);

                if (patienInvoiceCount)
                    return Unauthorized();

                var patientExists = await _patient.GetIndividualPatient(patientId);

                if (patientExists == null)
                    return NotFound();

                var result = await _patient.DeletePatient(patientId);

                if (result > 0)
                    return Ok("Patient Has Been Deleted!");

                return BadRequest("An Error Has Occurred");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updatepatient")]
        public async Task<IHttpActionResult> UpdatePatient(PatientDTO currentPatient)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var patientExists = await _patient.GetIndividualPatient(currentPatient.PatientId);

                    if (patientExists == null)
                        return NotFound();

                    var result = await _patient.UpdatePatient(currentPatient);

                    if (result > 0)
                        return Ok("Patient Has Been Updated!");

                    return BadRequest("An Error Has Occurred");
                }
                else
                {
                    return BadRequest("An Error Has Occurred");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
