using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repository.Interface;
using Telecomm360.Service.Interface;

namespace Telecomm360.Service.Implementation;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request)
    {
        //  Mapping DTO → Model
        var customer = new Customer                                // request = { "Name" : "Ram", "Type" : "Individual", "KYCStatus" : "Pending", "ContactInfo" : "}
        {
            Name = request.Name,
            Type = request.Type,
            KYCStatus = request.KYCStatus,
            ContactInfo = request.ContactInfo
        };

        var result = await _repository.CreateCustomerAsync(customer);

        // Mapping Model → DTO  Reverse Mapping to send response back to controller 
        return new CustomerResponseDto
        {                                                            // customer = { CustomerId = 1, Name = "Ram", Type = "Individual", KYCStatus = "Pending", ContactInfo = "" }
            CustomerId = result.CustomerId,
            Name = result.Name,
            Type = result.Type,
            KYCStatus = result.KYCStatus,
            ContactInfo = result.ContactInfo
        };
    }

    public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
    {
        var customers = await _repository.GetAllCustomersAsync();

        return customers.Select(c => new CustomerResponseDto
        {
            CustomerId = c.CustomerId,
            Name = c.Name,
            Type = c.Type,
            KYCStatus = c.KYCStatus,
            ContactInfo = c.ContactInfo
        }).ToList();
    }

    public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int customerId)
    {
        var customer = await _repository.GetCustomerByIdAsync(customerId);

        if (customer == null)
            return null;

        return new CustomerResponseDto
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Type = customer.Type,
            KYCStatus = customer.KYCStatus,
            ContactInfo = customer.ContactInfo
        };
    }

    public async Task<CustomerResponseDto?> UpdateCustomerAsync(int customerId, UpdateCustomerRequestDto request)
    {
        var existing = await _repository.GetCustomerByIdAsync(customerId);

        if (existing == null)
            return null;

        // Mapping update
        existing.Name = request.Name;
        existing.Type = request.Type;
        existing.KYCStatus = request.KYCStatus;
        existing.ContactInfo = request.ContactInfo;

        var updated = await _repository.UpdateCustomerAsync(existing);

        return new CustomerResponseDto
        {
            CustomerId = updated!.CustomerId,
            Name = updated.Name,
            Type = updated.Type,
            KYCStatus = updated.KYCStatus,
            ContactInfo = updated.ContactInfo
        };
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        return await _repository.DeleteCustomerAsync(customerId);
    }
}
