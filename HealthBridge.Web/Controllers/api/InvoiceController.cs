using HealthBridge.BusinessLogic.DTO;
using HealthBridge.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HealthBridge.Web.Controllers.api
{

    // Thin Controller, Fat Service Manager Approach
    [RoutePrefix("api/Invoices")]
    public class InvoiceController : ApiController
    {
        private IInvoice _invoice;

        public InvoiceController(IInvoice invoice)
        {
            _invoice = invoice;
        }

        [HttpGet]
        [Route("getallinvoices")]
        public async Task<IHttpActionResult> GetAllInvoices()
        {
            try
            {
                var invoices = await _invoice.GetAllInvoices();

                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getinvoicebyinvoiceid/{invoiceId}")]
        public async Task<IHttpActionResult> GetInvoiceByInvoiceId(long invoiceId)
        {
            try
            {
                var invoice = await _invoice.GetIndividualInvoice(invoiceId);

                if (invoice == null)
                    return NotFound();

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteInvoice/{invoiceId}")]
        public async Task<IHttpActionResult> DeleteInvoice(long invoiceId)
        {
            try
            {
                var invoiceExists = await _invoice.GetIndividualInvoice(invoiceId);

                if (invoiceExists != null)
                    return NotFound();

                var result = await _invoice.DeleteInvoice(invoiceId);

                if (result > 0)
                    return Ok("Invoice has been deleted");

                return BadRequest("An Error Has Occurred");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("savenewinvoice")]
        public async Task<IHttpActionResult> SaveInvoice(CompoundInvoiceDTO compundInvoice)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _invoice.SaveInvoice(compundInvoice);

                    if (result > 0)
                        return Ok("Invoice Saved To Database!");
                    else
                        return BadRequest("An Error Has Occurred");
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);

                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updatepreviousinvoice")]
        public async Task<IHttpActionResult> UpdateInvoice(InvoiceUpdateDTO compundInvoice)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _invoice.UpdateInvoice(compundInvoice);

                    if (result > 0)
                        return Ok("Invoice Saved To Database!");
                    else
                        return BadRequest("An Error Has Occurred");
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);

                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getinvoicewithlineitems/{invoiceId}")]
        public async Task<IHttpActionResult> GetInvoiceWithLineItems(long invoiceId)
        {
            try
            {
                var result = await _invoice.GetInvoiceWithLineItems(invoiceId);

                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
