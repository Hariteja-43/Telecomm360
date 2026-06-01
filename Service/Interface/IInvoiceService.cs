using Telecomm360.DTOs;

namespace Telecomm360.Services.Interfaces;

public interface IInvoiceServices
{
    List<InvoiceDto> GetAllInvoice(SearchDto search);
    InvoiceDto GetInvoiceById(int Invoiceid);
    List<InvoiceDto> GetInvoiceByCustomer(string customerId);
    InvoiceDto CreateInvoice(InvoiceDto Invoicedto);
    InvoiceDto UpdateInvoice(int Invoiceid, decimal Invoiceamount);
    InvoiceDto FinalizeInvoice(int Invoiceid);
}