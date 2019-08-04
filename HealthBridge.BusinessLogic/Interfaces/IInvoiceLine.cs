using HealthBridge.BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthBridge.BusinessLogic.Interfaces
{
    public interface IInvoiceLine
    {
        Task SaveInvoiceLineItems(List<InvoiceLineDTO> InvoiceLineItems, long InvoiceId);
        Task UpdateInvoiceLineItems(InvoiceLineDTO InvoiceLineItem);
        Task<decimal> CalculateInvoiceTotal(long InvoiceId);
        Task AddNewItemToExistingInvoiceLineItems(InvoiceLineDTO newLineItem);
        //Task DeleteLineItem(long InvoiceLineID);
    }
}
