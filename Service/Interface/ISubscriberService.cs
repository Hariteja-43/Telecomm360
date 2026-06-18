using Telecomm360.DTO;

namespace Telecomm360.Service.Interface
{
    public interface ISubscriberService
    {
        // Create Subscriber
        Task<SubscriberResponseDto> CreateSubscriberAsync(CreateSubscriberRequestDto request);

        // Get All Subscribers
        Task<List<SubscriberResponseDto>> GetAllSubscribersAsync();

        //  Get Subscriber by Id
        Task<SubscriberResponseDto?> GetSubscriberByIdAsync(int subscriberId);

        //  Update Subscriber
        Task<SubscriberResponseDto?> UpdateSubscriberAsync(int subscriberId, UpdateSubscriberRequestDto request);
        //  Delete Subscriber
        Task<bool> DeleteSubscriberAsync(int subscriberId);
        Task UpdateSubscriberAsync(int id, CreateSubscriberRequestDto dto);
    }
}
