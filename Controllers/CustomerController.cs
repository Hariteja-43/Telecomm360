using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTO;
using Telecomm360.Service.Interface;
using Telecomm360.Constants;

namespace Telecomm360.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    // creating a new customer
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequestDto request)
    {
        if (!ModelState.IsValid)
        {
             return BadRequest(GeneralConstants.InvalidInput);
        }
           

        var customer_created = await _service.CreateCustomerAsync(request);

        // Message  will be sent in response body along with the created customer details

        return Ok(new
        {
            // message = CustomerConstants.Created,
            data = customer_created
        });
    }

    // Getting list of all customers
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var result = await _service.GetAllCustomersAsync();

        return Ok(new
        {
            message = GeneralConstants.OperationSuccess,
            data = result
        });
    }

    // Getting details of a specific customer using its id along with ID validation < = 0
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        if (customerId <= 0)
        {
            return BadRequest(GeneralConstants.InvalidInput); 
        }

        var customer = await _service.GetCustomerByIdAsync(customerId);

        if (customer == null)
            return NotFound(CustomerConstants.NotFound);

        return Ok(new
        {
            data = customer
        });
    }

    // Updating a specific customer along with ID validation < = 0
    [HttpPut("{customerId}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomerRequestDto request)
    {
        if (customerId <= 0)
        {
            return BadRequest(GeneralConstants.InvalidInput); 
        }

        var updated = await _service.UpdateCustomerAsync(customerId, request);

        if (updated == null)
            return NotFound(CustomerConstants.NotFound);

        return Ok(new
        {
            data = updated
        });
    }

    // Deleting a specific customer along with ID validation < = 0
    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        if (customerId <= 0)
        {
            return BadRequest(GeneralConstants.InvalidInput); 
        }

        var deleted = await _service.DeleteCustomerAsync(customerId);

        if (!deleted)
            return NotFound(CustomerConstants.NotFound);

        return Ok();
    }
}
