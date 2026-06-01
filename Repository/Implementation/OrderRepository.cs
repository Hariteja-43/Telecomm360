using Microsoft.EntityFrameworkCore;
using Telecom360.Data;
using Telecom360.DTO.Order;
using Telecom360.Models;
using Telecom360.Repository.Interface;



namespace Telecom360.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL ORDERS
        public async Task<IEnumerable<Order>> GetAllOrders(OrderResponseDto order)
        {
            return await _context.Orders.ToListAsync();
        }

        // ✅ GET ORDER BY ID
        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        // ✅ CREATE ORDER
        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        // ✅ UPDATE ORDER
        public async Task<Order> UpdateOrder(Order order)
        {
            var existing = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderID == order.OrderID);

            if (existing == null)
                return null;

            // ✅ Update fields
            existing.SubscriberID = order.SubscriberID;
            existing.ProductID = order.ProductID;
            existing.OrderDate = order.OrderDate;
            existing.Status = order.Status;
            existing.FulfillmentSteps = order.FulfillmentSteps;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}