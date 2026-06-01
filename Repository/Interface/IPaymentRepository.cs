using Telecomm360.DTOs;
using Telecomm360.Models;

namespace Telecomm360.Repository.Interfaces;

public interface IPaymentRepository
{
    List<PaymentDto> GetAllPayment();
    PaymentDto? GetPaymentById(int Paymentid);
    PaymentDto CreatePayment(PaymentDto payment);
    void UpdatePayment(PaymentDto payment);
}