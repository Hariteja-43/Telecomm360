using Telecomm360.DTOs;
using Telecomm360.Models;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Services.Interfaces;

namespace Telecomm360.Services;

public class InvoiceServices : IInvoiceServices
{
    private readonly IInvoiceRepository _repo;

    public InvoiceServices(IInvoiceRepository repo)
    {
        _repo = repo;
    }

    // Convert Entity → DTO
    private InvoiceDto MapToDto(Invoice i)
    {
        return new InvoiceDto
        {
            InvoiceID = i.InvoiceID,
            CustomerID = i.CustomerID,
            Amount = i.Amount,
            Status = i.Status
        };
    }

    //  GET ALL
    public List<InvoiceDto> GetAllInvoice(SearchDto search)
    {
        // Filtering not implemented in repository yet; returning all for now.
        // Keeps API signature consistent with SearchDto.
        return _repo.GetAllInvoice()
            .Select(i => MapToDto(i))
            .ToList();
    }


    //  GET BY ID
    public InvoiceDto GetInvoiceById(int Invoiceid)
    {
        var inv = _repo.GetInvoiceById(Invoiceid);
        if (inv == null) return null;

        return MapToDto(inv);
    }

    // GET BY CUSTOMER
    public List<InvoiceDto> GetInvoiceByCustomer(string customerId)
    {
        return _repo.GetInvoiceByCustomer(customerId)
            .Select(i => MapToDto(i))
            .ToList();
    }

    //  CREATE DTO → Entity
    public InvoiceDto CreateInvoice(InvoiceDto Invoicedto)
    {
        var invoice = new Invoice
        {           
            CustomerID = Invoicedto.CustomerID,
            Amount = Invoicedto.Invoiceamount,
            Status = "DRAFT" // Enum
        };

        var invoiceId = _repo.CreateInvoice(invoice);

        Invoicedto.InvoiceID = invoiceId; // Update DTO with generated ID

        return Invoicedto;
    }

    // Update
    public InvoiceDto UpdateInvoice(int invoiceId, decimal invoiceAmount)
    {
        var inv = _repo.GetInvoiceById(invoiceId);
        if (inv == null) return null;
    
        inv.Amount += invoiceAmount;
        _repo.UpdateInvoice(inv);

        return MapToDto(inv);
    }

    //  FINALIZE
    public InvoiceDto FinalizeInvoice(int Invoiceid)
    {
        var inv = _repo.GetInvoiceById(Invoiceid);
        if (inv == null) return null;

        inv.Status = "FINALIZED";
        _repo.UpdateInvoice(inv);
        return MapToDto(inv);
    }

    // Kept for backward compatibility with older controller code.
    public List<InvoiceDto> GetAllInvoice()
        => GetAllInvoice(default!);

}