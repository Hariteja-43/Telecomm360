using Telecomm360.Models;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Services.Interfaces;
using System;
using Telecomm360.DTOs;

namespace Telecomm360.Services;

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