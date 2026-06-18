using Telecomm360.Model;

namespace Telecomm360.Repository.Interfaces;

public interface IInvoiceRepository
{
    List<Invoice> GetAllInvoice();
    Invoice GetInvoiceById(int Invoiceid);
    List<Invoice> GetInvoiceByCustomer(string customerId);
    int CreateInvoice(Invoice invoice);
    void UpdateInvoice(Invoice invoice);
}