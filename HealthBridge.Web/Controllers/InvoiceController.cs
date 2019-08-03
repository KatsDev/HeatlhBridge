using HealthBridge.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HealthBridge.Web.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InvoiceList()
        {
            try
            {
                List<InvoiceDTO> invoiceList = new List<InvoiceDTO>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44305/api/");

                    var responseTask = client.GetAsync("Invoices/getallinvoices");

                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<InvoiceDTO>>();

                        readTask.Wait();

                        invoiceList = readTask.Result;
                    }
                }
                return View(invoiceList);
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
                using (var client = new HttpClient())
                {
                    List<PatientDropDownDTO> patientDropDownDisplay = new List<PatientDropDownDTO>();

                    client.BaseAddress = new Uri("https://localhost:44305/api/");

                    var responseTask = client.GetAsync("Patients/getpatientsdropdowndisplay");

                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<PatientDropDownDTO>>();

                        readTask.Wait();

                        patientDropDownDisplay = readTask.Result;

                        ViewBag.PatientDropDown = patientDropDownDisplay;
                    }
                }
                return View("InvoiceForm", new CompoundInvoiceDTO());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult EditPatientInvoice(long invoiceId)
        {
            try
            {
                CompoundInvoiceDTO compoundInvoice = new CompoundInvoiceDTO();

                using (var client = new HttpClient())
                {
                    List<PatientDropDownDTO> patientDropDownDisplay = new List<PatientDropDownDTO>();

                    client.BaseAddress = new Uri("https://localhost:44305/api/");

                    var responseTask = client.GetAsync("Patients/getpatientsdropdowndisplay");

                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<PatientDropDownDTO>>();

                        readTask.Wait();

                        patientDropDownDisplay = readTask.Result;

                        ViewBag.PatientDropDown = patientDropDownDisplay;
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44305/api/");
                    var responseTask = client.GetAsync("Invoices/getinvoicewithlineitems/" + invoiceId);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<CompoundInvoiceDTO>();

                        readTask.Wait();

                        compoundInvoice = readTask.Result;
                    }
                }
                return View("InvoiceForm", compoundInvoice);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}