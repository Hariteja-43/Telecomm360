using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.Model;
using Telecomm360.Repository.Interface;

namespace Telecomm360.Repository.Implementation;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();


        return customer;
    }
    

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
    }

    public async Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        _context.Customers.Update(customer); // Telling EF Core to update the existing customer record
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);

        if (customer == null)
            return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return true;
    }
}
