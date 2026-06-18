using Telecom360.DTO.Order;
using Telecom360.Models;

namespace Telecom360.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders(OrderResponseDto order);
        Task<Order> GetOrderById(int orderId);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order);
    }
}
