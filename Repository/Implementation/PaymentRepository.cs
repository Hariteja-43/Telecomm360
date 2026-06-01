
using Telecomm360.Data;
using Telecomm360.Models;
using Telecomm360.Repository.Interfaces;
using Telecomm360.DTOs;

namespace Telecomm360.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    private PaymentDto ToDto(Payment p) => new PaymentDto
    {
        PaymentID = p.PaymentID,
        InvoiceID = p.InvoiceID,
        Amount = p.Amount,
        Date = p.Date,
        Method = p.Method,
        Status = p.Status
    };

    private Payment ToEntity(PaymentDto d) => new Payment
    {
        PaymentID = d.PaymentID,
        InvoiceID = d.InvoiceID,
        Amount = d.Amount,
        Date = d.Date,
        Method = d.Method,
        Status = d.Status
    };

    public List<PaymentDto> GetAllPayment()
        => _context.Payments.ToList().Select(p => ToDto(p)).ToList();

    public PaymentDto? GetPaymentById(int Paymentid)
        => _context.Payments.FirstOrDefault(x => x.PaymentID == Paymentid) is Payment p ? ToDto(p) : null;

    public PaymentDto CreatePayment(PaymentDto payment)
    {
        var entity = ToEntity(payment);
        _context.Payments.Add(entity);
        _context.SaveChanges();
        return ToDto(entity);
    }

    public void UpdatePayment(PaymentDto payment)
    {
        var entity = ToEntity(payment);
        _context.Payments.Update(entity);
        _context.SaveChanges();
    }
}