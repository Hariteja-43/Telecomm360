using Telecom360.Model;

namespace Telecom360.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();

        Task<Order?> GetOrderById(int orderId);

        Task<Order> CreateOrder(Order order);

        Task<Order?> UpdateOrder(Order order);

        Task<bool> DeleteOrder(int orderId);
    }
}
