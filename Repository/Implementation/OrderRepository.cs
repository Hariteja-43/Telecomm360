using Microsoft.EntityFrameworkCore;
using Telecom360.Model;
using Telecom360.Repository.Interface;
using Telecomm360.Data;

namespace Telecom360.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrder(Order order)
        {
            var existing = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderID == order.OrderID);

            if (existing == null)
                return null;

            if (order.SubscriberID != 0)
                existing.SubscriberID = order.SubscriberID;

            if (order.ProductID != 0)
                existing.ProductID = order.ProductID;

            if (order.OrderDate != default)
                existing.OrderDate = order.OrderDate;

            if (!string.IsNullOrWhiteSpace(order.Status))
                existing.Status = order.Status;

            if (!string.IsNullOrWhiteSpace(order.FulfillmentSteps))
                existing.FulfillmentSteps = order.FulfillmentSteps;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
                return false;

            _context.Orders.Remove(order);
            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
    }
}
