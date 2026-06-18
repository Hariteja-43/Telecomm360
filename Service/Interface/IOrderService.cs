using Telecom360.DTO.Order;

namespace Telecom360.Service.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrders();

        Task<OrderResponseDto?> GetOrderById(int orderId);

        Task<OrderResponseDto> CreateOrder(CreateOrderRequestDto request);

        Task<OrderResponseDto?> UpdateOrder(int orderId, UpdateOrderRequestDto request);

        Task<bool> CancelOrder(int orderId);

        Task<bool> SubmitOrder(int orderId);

        Task<bool> FulfillOrder(int orderId);
    }
}
