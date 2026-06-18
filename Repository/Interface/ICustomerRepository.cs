using Telecomm360.Model;

namespace Telecomm360.Repository.Interface;

public interface ICustomerRepository
{
    Task<Customer> CreateCustomerAsync(Customer customer);

    Task<List<Customer>> GetAllCustomersAsync();

    Task<Customer?> GetCustomerByIdAsync(int customerId);

    Task<Customer?> UpdateCustomerAsync(Customer customer);

    Task<bool> DeleteCustomerAsync(int customerId);
}