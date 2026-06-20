using Microsoft.AspNetCore.Mvc;
using Telecom360.Service.Interface;
using Telecom360.DTO.Order;
using Telecom360.Constant;
using Microsoft.AspNetCore.Authorization;

namespace Telecom360.Controllers
{
    [ApiController]
    [Route("orders")]
     [Authorize(Roles = "ProductManager")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        // GET /orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _service.GetAllOrders();
            return Ok(orders ?? new List<OrderResponseDto>());
        }

        // GET /orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var order = await _service.GetOrderById(id);

            if (order == null)
                return NotFound(ErrorMessages.NOT_FOUND);

            return Ok(order);
        }

        // POST /orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdOrder = await _service.CreateOrder(request);

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = createdOrder.OrderID },
                createdOrder
            );
        }

        // PUT /orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedOrder = await _service.UpdateOrder(id, request);

            if (updatedOrder == null)
                return NotFound(ErrorMessages.NOT_FOUND);

            return Ok(updatedOrder);
        }

        // DELETE /orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int id)
        {
            var success = await _service.CancelOrder(id);

            if (!success)
                return NotFound(ErrorMessages.NOT_FOUND);

            return Ok(new OrderActionResponseDto
            {
                OrderID = id,
                Status = "CANCELLED",
                Message = "Order cancelled successfully"
            });
        }

        // POST /orders/{id}/submit
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitOrder([FromRoute] int id)
        {
            var result = await _service.SubmitOrder(id);

            if (!result)
                return BadRequest("Order cannot be submitted in current state");

            return Ok(new OrderActionResponseDto
            {
                OrderID = id,
                Status = "SUBMITTED",
                Message = "Order submitted successfully"
            });
        }

        // POST /orders/{id}/fulfill
        [HttpPost("{id}/fulfill")]
        public async Task<IActionResult> FulfillOrder([FromRoute] int id)
        {
            var result = await _service.FulfillOrder(id);

            if (!result)
                return BadRequest("Order cannot be fulfilled in current state");

            return Ok(new OrderActionResponseDto
            {
                OrderID = id,
                Status = "FULFILLED",
                Message = "Order fulfilled successfully"
            });
        }
    }
}