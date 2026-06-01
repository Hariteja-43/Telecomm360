using Telecom360.DTO;
using Telecom360.DTO.Order;

namespace Telecom360.Services.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDto>> GetAllOrders(OrderResponseDto order);

        Task<OrderResponseDto> GetOrderById( int orderId);

        Task<OrderResponseDto> CreateOrder(CreateOrderRequestDto request);

        Task<OrderResponseDto> UpdateOrder(int orderId, UpdateOrderRequestDto request);
        Task<bool> CancelOrder(int orderId);

        Task<bool> SubmitOrder(int orderId);

        Task<bool> FulfillOrder(int orderId);
    }
}
