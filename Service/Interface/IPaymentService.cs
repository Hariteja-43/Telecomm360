using System;
using Telecomm360.DTOs;



namespace Telecomm360.Services.Interfaces;

public interface IPaymentService
{
    List<PaymentDto> GetAllPayment(SearchDto search);

    PaymentDto GetPaymentById(int Paymentid);
    // Change from: List<PaymentDto> CreatePayment(PaymentDto payment);
// Change to:
    PaymentDto CreatePayment(PaymentDto payment);
    PaymentDto Reconcile(int Paymentid);

    // Change from: List<PaymentDto> CreatePayment(PaymentDto payment);
// Change to:

}
