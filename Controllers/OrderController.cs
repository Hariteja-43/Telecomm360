using Microsoft.AspNetCore.Mvc;
using Telecom360.Services.Interface;
using Telecom360.DTO.Order;
using Telecom360.Constant;
using Telecom360.Models;

namespace Telecom360.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        //  GET /orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders(OrderResponseDto order)
        {
            try
            {
                var orders = await _service.GetAllOrders(order);
                return Ok(orders);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        //  GET /orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var order = await _service.GetOrderById(orderId);
                return order == null ? NotFound(ErrorMessages.NOT_FOUND) : Ok(order);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        //  POST /orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            try
            {
                var createdOrder = await _service.CreateOrder(request);
                return Ok(createdOrder);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        //  PUT /orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderRequestDto order)
        {
            try
            {
                var updatedOrder = await _service.UpdateOrder(orderId, order);
                return updatedOrder == null ? NotFound(ErrorMessages.NOT_FOUND) : Ok(updatedOrder);
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        //  DELETE /orders/{id} → Cancel order
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                var success = await _service.CancelOrder(orderId);
                return !success ? NotFound(ErrorMessages.NOT_FOUND) : Ok("Order cancelled successfully");
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        // ✅ POST /orders/{id}/submit → Trigger orchestration
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitOrder(int orderId)
        {
            try
            {
                var result = await _service.SubmitOrder(orderId);
               return !result ? NotFound(ErrorMessages.NOT_FOUND) : Ok(new OrderActionResponseDto
                                {
                                    Status = "SUBMITTED",
                                    Message = "Order submitted successfully"
                                });
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }

        // ✅ POST /orders/{id}/fulfill → Mark fulfilled
        [HttpPost("{id}/fulfill")]
        public async Task<IActionResult> FulfillOrder(int orderId)
        {
            try
            {
                var result = await _service.FulfillOrder(orderId);
                return !result ? NotFound(ErrorMessages.NOT_FOUND) : Ok(new OrderActionResponseDto
                                                {
                                                    OrderID = orderId,
                                                    Status = "FULFILLED",
                                                    Message = "Order fulfilled successfully"
                                                });
            }
            catch
            {
                return StatusCode(500, ErrorMessages.SERVER_ERROR);
            }
        }
    }
}