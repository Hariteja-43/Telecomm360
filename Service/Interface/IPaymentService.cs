using Telecomm360.DTO;
using Telecomm360.DTOs;



namespace Telecomm360.Service.Interfaces
{

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
}
