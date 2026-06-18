using Telecomm360.Repository.Interfaces;
using Telecomm360.Service.Interfaces;
using Telecomm360.DTOs;
using Telecomm360.DTO;

namespace Telecomm360.Service.Implementation
{   

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repo;
    private readonly IInvoiceRepository _invoiceRepo;

    public PaymentService(IPaymentRepository repo, IInvoiceRepository invoiceRepo)
    {
        _repo = repo;
        _invoiceRepo = invoiceRepo;
    }

    public List<PaymentDto> GetAllPayment(SearchDto search)
        => _repo.GetAllPayment();


    public PaymentDto? GetPaymentById(int Paymentid)
        => _repo.GetPaymentById(Paymentid);

    public PaymentDto CreatePayment(PaymentDto Paymentdto)
        => _repo.CreatePayment(Paymentdto);
    public PaymentDto? Reconcile(int Paymentid)
    {
        var pay = _repo.GetPaymentById(Paymentid);
        if (pay == null) return null;

        pay.Status = "RECONCILED";
        _repo.UpdatePayment(pay);

        var invoice = _invoiceRepo.GetInvoiceById(pay.InvoiceID);
        if (invoice != null)
        {
            invoice.Status = "PAID";
            _invoiceRepo.UpdateInvoice(invoice);
        }

        return pay;
    }
}
}