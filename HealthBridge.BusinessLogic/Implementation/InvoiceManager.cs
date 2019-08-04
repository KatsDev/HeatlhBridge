using HealthBridge.BusinessLogic.DTO;
using HealthBridge.BusinessLogic.Helper;
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
    public class InvoiceManager : IInvoice
    {
        private IGenericRepository<Invoice> _invoiceRepository = null;
        private IGenericRepository<InvoiceLine> _invoiceLineRepository = null;
        private IInvoiceLine _invoiceManager;

        public InvoiceManager()
        {
            this._invoiceRepository = new GenericRepository<Invoice>();
            this._invoiceLineRepository = new GenericRepository<InvoiceLine>();
            this._invoiceManager = new InvoiceLineManager();
        }

        public async Task<List<InvoiceDTO>> GetAllInvoices()
        {
            try
            {
                var invoiceList = new List<InvoiceDTO>();
                var invoicesInDB = await _invoiceRepository.GetAll();

                foreach (var invoice in invoicesInDB)
                {
                    InvoiceDTO invoiceDTO = new InvoiceDTO();
                    invoiceDTO.InvoiceId = invoice.InvoiceId;
                    invoiceDTO.InvoiceDateTime = invoice.InvoiceDateTime;
                    invoiceDTO.PatientId = invoice.PatientId;
                    invoiceDTO.InvoiceTotal = invoice.InvoiceTotal;
                    invoiceDTO.patient.FirstName = invoice.Patient.FirstName;
                    invoiceDTO.patient.LastName = invoice.Patient.LastName;
                    invoiceList.Add(invoiceDTO);
                }

                return invoiceList.OrderBy(x => x.InvoiceId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InvoiceDTO> GetIndividualInvoice(long invoiceId)
        {
            try
            {
                InvoiceDTO invoice = new InvoiceDTO();

                var invoiceInDb = await _invoiceRepository.GetById(invoiceId);

                if (invoiceInDb != null)
                {
                    invoice.InvoiceDateTime = invoiceInDb.InvoiceDateTime;
                    invoice.InvoiceId = invoiceInDb.InvoiceId;
                    invoice.InvoiceTotal = invoiceInDb.InvoiceTotal;
                    invoice.PatientId = invoiceInDb.PatientId;
                    return invoice;
                }
                else
                {
                    invoice = null;
                    return invoice;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> SaveInvoice(CompoundInvoiceDTO compoundInvoice)
        {
            try
            {
                Invoice invoiceDB = new Invoice();

                decimal invoiceTotalAmount = compoundInvoice.InvoiceLineItems.Sum(item => item.LineTotal);

                invoiceDB.InvoiceDateTime = DateTime.Now;
                invoiceDB.PatientId = compoundInvoice.InvoiceDetails.PatientId;
                invoiceDB.InvoiceTotal = invoiceTotalAmount;

                _invoiceRepository.Insert(invoiceDB);

                var invoiceSaveResult = await _invoiceRepository.Save();

                compoundInvoice.InvoiceDetails.InvoiceId = invoiceDB.InvoiceId;

                await _invoiceManager.SaveInvoiceLineItems(compoundInvoice.InvoiceLineItems, compoundInvoice.InvoiceDetails.InvoiceId);

                return invoiceSaveResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteInvoice(long invoiceId)
        {
            try
            {
                List<InvoiceLine> invoiceLine = new List<InvoiceLine>();

                var allInvoiceLines = await _invoiceLineRepository.Search(x => x.InvoiceId == invoiceId);

                foreach (var invoiceLineItems in allInvoiceLines.Where(x => x.InvoiceId == invoiceId))
                {
                    _invoiceLineRepository.Delete(invoiceLineItems.InvoiceLineId);
                }

                await _invoiceLineRepository.Save();

                _invoiceRepository.Delete(invoiceId);

                var result = await _invoiceRepository.Save();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteInvoiceLineItem(long InvoiceLineID)
        {
            try
            {
                var invoiceLineInDB = await _invoiceLineRepository.Search(x => x.InvoiceLineId == InvoiceLineID);

                _invoiceLineRepository.Delete(invoiceLineInDB);

                var result = await _invoiceLineRepository.Save();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateInvoice(InvoiceUpdateDTO compoundInvoice)
        {
            try
            {
                Invoice invoiceInDB = new Invoice();
                IList<InvoiceLine> invoiceLineInDB = null;
                InvoiceLine invoiceLineItemToUpdate = new InvoiceLine();

                // Check if patient exists - update if exists else exit with exception

                invoiceInDB = await _invoiceRepository.GetById(compoundInvoice.InvoiceDetails.InvoiceId);
                invoiceLineInDB = await _invoiceLineRepository.Search(x => x.InvoiceLineId == compoundInvoice.InvoiceLineItem.InvoiceLineId);

                await _invoiceManager.UpdateInvoiceLineItems(compoundInvoice.InvoiceLineItem);

                decimal invoiceTotalAmount = await _invoiceManager.CalculateInvoiceTotal(compoundInvoice.InvoiceDetails.InvoiceId);

                invoiceInDB.InvoiceTotal = invoiceTotalAmount;
                invoiceInDB.InvoiceDateTime = DateTime.Now;

                _invoiceRepository.Update(invoiceInDB);

                var result = await _invoiceRepository.Save();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); ;
            }
        }

        public async Task<bool> patientInvoiceExists(long patientId)
        {
            try
            {
                var patientInvoice = await _invoiceRepository.Search(x => x.PatientId == patientId);

                if (patientInvoice.Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompoundInvoiceDTO> GetInvoiceWithLineItems(long invoiceId)
        {
            try
            {
                CompoundInvoiceDTO compoundInvoice = new CompoundInvoiceDTO();

                var invoiceExists = await _invoiceRepository.Search(x => x.InvoiceId == invoiceId);

                if (invoiceExists.Count > 0)
                {
                    var invoiceHeader = await _invoiceRepository.GetById(invoiceId);
                    var invoiceLineItems = await _invoiceLineRepository.Search(x => x.InvoiceId == invoiceId);

                    compoundInvoice.InvoiceDetails.InvoiceDateTime = invoiceHeader.InvoiceDateTime;
                    compoundInvoice.InvoiceDetails.InvoiceId = invoiceHeader.InvoiceId;
                    compoundInvoice.InvoiceDetails.InvoiceTotal = invoiceHeader.InvoiceTotal;
                    compoundInvoice.InvoiceDetails.PatientId = invoiceHeader.PatientId;

                    foreach (var lineItem in invoiceLineItems)
                    {
                        InvoiceLineDTO lineItemsToAdd = new InvoiceLineDTO();
                        lineItemsToAdd.Code = lineItem.Code;
                        lineItemsToAdd.Description = lineItem.Description;
                        lineItemsToAdd.InvoiceId = lineItem.InvoiceId;
                        lineItemsToAdd.InvoiceLineId = lineItem.InvoiceLineId;
                        lineItemsToAdd.LineTotal = Math.Round(lineItem.LineTotal, 2);
                        var Qty = Convert.ToDecimal(lineItem.Qty);
                        var QTYConvert = DataConverter.ToSingle(Qty);
                        lineItemsToAdd.Qty = QTYConvert;

                        compoundInvoice.InvoiceLineItems.Add(lineItemsToAdd);
                    }

                    return compoundInvoice;
                }
                else
                {
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
