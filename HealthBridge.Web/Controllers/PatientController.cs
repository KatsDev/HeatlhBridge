using HealthBridge.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HealthBridge.Web.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PatientList()
        {
            try
            {
                List<PatientDTO> patientList = new List<PatientDTO>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44305/api/");
                    var responseTask = client.GetAsync("Patients/getallpatients");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if(result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<PatientDTO>>();

                        readTask.Wait();

                        patientList = readTask.Result;
                    }
                }
                ViewBag.PatientCount = patientList.Count;
                return View(patientList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult New()
        {
            try
            {
                return View("PatientForm", new PatientDTO());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Edit(long patientId)
        {
            try
            {
                PatientDTO patientData = new PatientDTO();
                patientData.PatientId = patientId;
                return View("PatientForm", patientData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}