using Telecomm360.DTO;

namespace Telecomm360.Service.Interface
{

public interface ICustomerService
{
    Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request);

    Task<List<CustomerResponseDto>> GetAllCustomersAsync();

    Task<CustomerResponseDto?> GetCustomerByIdAsync(int customerId);

    Task<CustomerResponseDto?> UpdateCustomerAsync(int customerId, UpdateCustomerRequestDto request);

    Task<bool> DeleteCustomerAsync(int customerId);
}
}
