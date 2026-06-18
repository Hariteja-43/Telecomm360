using Microsoft.AspNetCore.Mvc;
using Telecomm360.Service.Interfaces;
using Telecomm360.DTOs;
using Telecomm360.Constants;
using Telecomm360.DTO;

namespace Telecomm360.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceServices _service;

    public InvoiceController(IInvoiceServices service)
    {
        _service = service;
    }

    //  GET all invoices → returns InvoiceDto
    [HttpGet]
    public IActionResult GetAll([FromQuery] SearchDto search)
    {
        return Ok(_service.GetAllInvoice(search));
    }

    // Creates the invoice
    [HttpPost]
   public IActionResult CreateInvoice(InvoiceDto invoiceDto)
    {
        if (invoiceDto == null)
        {
            return BadRequest(new { Message = InvoiceErrorMessages.InvalidInvoiceData });
        }
        
        var invoice = _service.CreateInvoice(invoiceDto);
        
        if (invoice == null)
        {
            return BadRequest(new { Message = InvoiceErrorMessages.InvoiceCreationFailed });
        }

        return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
    }
    // GET by ID
    [HttpGet("{id}")]
    public IActionResult GetInvoiceById(int id) // Renamed parameter to match route template "{id}"
    {
        if (id <= 0) 
        {
            return BadRequest(new { Message = InvoiceErrorMessages.InvalidInvoiceId });
        }
        
        var invoice = _service.GetInvoiceById(id);
        
        if (invoice == null)
        {
            return NotFound(new { Message = InvoiceErrorMessages.InvoiceNotFound });
        }

        return Ok(invoice);
    }
    // PUT 
    [HttpPut("{id}/adjust")]
   public IActionResult Adjust(int id, [FromQuery] decimal amount) // Renamed parameter to match route template "{id}"
    {
        if (id <= 0)
        {
            return BadRequest(new { Message = InvoiceErrorMessages.InvalidInvoiceId });
        }

        var data = _service.UpdateInvoice(id, amount);

        if (data == null)
        {
            return NotFound(new { Message = InvoiceErrorMessages.InvoiceNotFound });
        }

        return Ok(data);
    }

    //  PUT finalize
    [HttpPut("{id}/finalize")]
   public IActionResult Finalize(int id) // Renamed parameter to match route template "{id}"
    {
        if (id <= 0)
        {
            return BadRequest(new { Message = InvoiceErrorMessages.InvalidInvoiceId });
        }

        var data = _service.FinalizeInvoice(id);

        if (data == null)
        {
            return NotFound(new { Message = InvoiceErrorMessages.InvoiceNotFound });
        }

        return Ok(data);
    }

    //  GET by customer
    [HttpGet("customer/{customerId}")]
    public IActionResult GetByCustomer(string customerId)
    {
        return Ok(_service.GetInvoiceByCustomer(customerId));
    }
}