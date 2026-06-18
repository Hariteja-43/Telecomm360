using Telecomm360.Data;
using Telecomm360.Model;
using Telecomm360.Repository.Interfaces;

namespace Telecomm360.Repository;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Invoice> GetAllInvoice()
        => _context.Invoices.ToList();

    //  Use int 
    public Invoice? GetInvoiceById(int Invoiceid)
    => _context.Invoices.FirstOrDefault(x => x.InvoiceID == Invoiceid);


    public List<Invoice> GetInvoiceByCustomer(string customerId)
        => _context.Invoices
            .Where(x => x.CustomerID == customerId)
            .ToList();

    public int CreateInvoice(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        _context.SaveChanges();
        return invoice.InvoiceID;
    }

    public void UpdateInvoice(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        _context.SaveChanges();
    }
}