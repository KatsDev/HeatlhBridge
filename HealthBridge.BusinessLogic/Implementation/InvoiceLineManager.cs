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
    public class InvoiceLineManager : IInvoiceLine
    {
        private IGenericRepository<InvoiceLine> _invoiceLineRepository = null;

        public InvoiceLineManager()
        {
            this._invoiceLineRepository = new GenericRepository<InvoiceLine>();
        }

        public async Task SaveInvoiceLineItems(List<InvoiceLineDTO> InvoiceLineItems, long InvoiceId)
        {
            try
            {
                InvoiceLine invoiceLineDB = new InvoiceLine();
                foreach (var lineItem in InvoiceLineItems)
                {
                    invoiceLineDB.Code = lineItem.Code;
                    invoiceLineDB.Description = lineItem.Description;
                    invoiceLineDB.InvoiceId = InvoiceId;
                    invoiceLineDB.LineTotal = lineItem.LineTotal;
                    invoiceLineDB.Qty = lineItem.Qty;
                    _invoiceLineRepository.Insert(invoiceLineDB);

                    await _invoiceLineRepository.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateInvoiceLineItems(InvoiceLineDTO InvoiceLineItem)
        {
            try
            {
                IList<InvoiceLine> invoiceLineInDB = null;
                InvoiceLine invoiceLineItemToUpdate = new InvoiceLine();

                invoiceLineInDB = await _invoiceLineRepository.Search(x => x.InvoiceLineId == InvoiceLineItem.InvoiceLineId);

                if (invoiceLineInDB != null)
                {
                    invoiceLineItemToUpdate = await _invoiceLineRepository.SearchIndividual(x => x.InvoiceLineId == InvoiceLineItem.InvoiceLineId);

                    invoiceLineItemToUpdate.Code = InvoiceLineItem.Code;
                    invoiceLineItemToUpdate.Description = InvoiceLineItem.Description;
                    invoiceLineItemToUpdate.LineTotal = InvoiceLineItem.LineTotal;
                    invoiceLineItemToUpdate.Qty = InvoiceLineItem.Qty;

                    _invoiceLineRepository.Update(invoiceLineItemToUpdate);

                    await _invoiceLineRepository.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> CalculateInvoiceTotal(long InvoiceId)
        {
            try
            {
                var invoice = await _invoiceLineRepository.Search(x => x.InvoiceId == InvoiceId);
                return invoice.Sum(item => item.LineTotal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
