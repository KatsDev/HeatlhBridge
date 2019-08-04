using HealthBridge.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.Interfaces
{
    public interface IInvoice
    {
        Task<List<InvoiceDTO>> GetAllInvoices();
        Task<int> SaveInvoice(CompoundInvoiceDTO compoundInvoice);
        Task<int> DeleteInvoice(long invoiceId);
        Task<InvoiceDTO> GetIndividualInvoice(long invoiceId);
        Task<bool> patientInvoiceExists(long patientId);
        Task<int> UpdateInvoice(InvoiceUpdateDTO compoundInvoice);
        Task<int> DeleteInvoiceLineItem(long InvoiceLineID);
        Task<CompoundInvoiceDTO> GetInvoiceWithLineItems(long invoiceId);

        Task<int> AddItemToExistingInvoice(InvoiceUpdateDTO compoundInvoice);
    }
}
