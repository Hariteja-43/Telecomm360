using Microsoft.AspNetCore.Mvc;
using Telecomm360.DTOs;

using Telecomm360.Service.Interfaces;
using Telecomm360.Constants;
using Telecomm360.DTO;

namespace Telecomm360.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _service;

    public PaymentController(IPaymentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] SearchDto search) => Ok(_service.GetAllPayment(search));


    [HttpPost]
    public IActionResult Create([FromBody] PaymentDto payment)
    {
        if (payment == null)
        {
            return BadRequest(new { Message = PaymentErrorMessages.InvalidPaymentPayload } );
        }
        var created = _service.CreatePayment(payment);
        return CreatedAtAction(nameof(Get), new { id = created.PaymentID }, created);
    }

    [HttpGet("{id}")]
   public IActionResult Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { Message = PaymentErrorMessages.InvalidPaymentId });
        }

        var pay = _service.GetPaymentById(id);
        
        if (pay == null)
        {
            return NotFound(new { Message = PaymentErrorMessages.PaymentNotFound(id) });
        }

        return Ok(pay);
    }
    [HttpPut("{id}/reconcile")]
    public IActionResult Reconcile(int id)
    {
        if (id <= 0)
        {
            return BadRequest(new { Message = PaymentErrorMessages.InvalidPaymentId });
        }

        var pay = _service.Reconcile(id);
        
        if (pay == null)
        {
            return NotFound(new { Message = PaymentErrorMessages.PaymentNotFound(id) });
        }
        
        return Ok(pay);
    }
}