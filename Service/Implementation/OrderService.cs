using Telecom360.Services.Interface;
using Telecom360.Repository.Interface;
using Telecom360.DTO.Order;
using Telecom360.Models;

namespace Telecom360.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        // ✅ GET ALL ORDERS
        public async Task<IEnumerable<OrderResponseDto>> GetAllOrders(OrderResponseDto order)
        {
            var orders = await _repo.GetAllOrders(order);

            return orders.Select(o => new OrderResponseDto
            {
                OrderID = o.OrderID,
                SubscriberID = o.SubscriberID,
                ProductID = o.ProductID,
                OrderDate = o.OrderDate,
                Status = o.Status,
                FulfillmentSteps = o.FulfillmentSteps
            });
        }

        // ✅ GET ORDER BY ID
        public async Task<OrderResponseDto?> GetOrderById(int orderid)
        {
            var order = await _repo.GetOrderById(orderid);
            if (order == null) return null;

            return new OrderResponseDto
            {
                OrderID = order.OrderID,
                SubscriberID = order.SubscriberID,
                ProductID = order.ProductID,
                OrderDate = order.OrderDate,
                Status = order.Status,
                FulfillmentSteps = order.FulfillmentSteps
            };
        }

        // ✅ CREATE ORDER
        public async Task<OrderResponseDto> CreateOrder(CreateOrderRequestDto request)
        {
            var order = new Order
            {
                
                SubscriberID = request.SubscriberID,
                ProductID = request.ProductID,
                OrderDate = DateTime.UtcNow,
                Status = "CREATED",
                FulfillmentSteps = "INIT"
            };
            //name change Order
            var created = await _repo.CreateOrder(order);

            return new OrderResponseDto
            {
                OrderID = created.OrderID,
                SubscriberID = created.SubscriberID,
                ProductID = created.ProductID,
                OrderDate = created.OrderDate,
                Status = created.Status,
                FulfillmentSteps = created.FulfillmentSteps
            };
        }

        // ✅ UPDATE ORDER
        public async Task<OrderResponseDto> UpdateOrder(int orderId, UpdateOrderRequestDto request)
        {
            var existing = await _repo.GetOrderById(orderId);
            if (existing == null) return null;

            existing.Status = request.Status ?? existing.Status;
            existing.FulfillmentSteps = request.FulfillmentSteps ?? existing.FulfillmentSteps;

            var updated = await _repo.UpdateOrder(existing);

            return new OrderResponseDto
            {
                OrderID = updated.OrderID,
                SubscriberID = updated.SubscriberID,
                ProductID = updated.ProductID,
                OrderDate = updated.OrderDate,
                Status = updated.Status,
                FulfillmentSteps = updated.FulfillmentSteps
            };
        }

        // ✅ CANCEL ORDER (DELETE LOGIC)
        public async Task<bool> CancelOrder(int OrderId)
        {
            var existing = await _repo.GetOrderById(OrderId);
            if (existing == null) return false;

            existing.Status = "CANCELLED";
            await _repo.UpdateOrder(existing);

            return true;
        }

        // ✅ SUBMIT ORDER (TRIGGERS ORCHESTRATION)
        public async Task<bool> SubmitOrder(int orderId)
        {
            var existing = await _repo.GetOrderById(orderId);
            if (existing == null) return false;

            if (existing.Status == "CREATED" || existing.Status == "UPDATED")
            {
                existing.Status = "SUBMITTED";
                existing.FulfillmentSteps = "ORCHESTRATION_STARTED";

                await _repo.UpdateOrder(existing);
                return true;
            }

            return false;
        }

        // ✅ FULFILL ORDER
        public async Task<bool> FulfillOrder(int orderId)
        {
            var existing = await _repo.GetOrderById(orderId);
            if (existing == null) return false;

            if (existing.Status == "SUBMITTED")
            {
                existing.Status = "FULFILLED";
                existing.FulfillmentSteps = "COMPLETED";

                await _repo.UpdateOrder(existing);
                return true;
            }

            return false;
        }
    }
}
