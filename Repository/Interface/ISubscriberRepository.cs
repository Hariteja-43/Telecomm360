using Telecomm360.Model;

namespace Telecomm360.Repository
{
    public interface ISubscriberRepository
    {
        
        Task<Subscriber> AddSubscriberAsync(Subscriber subscriber);

        
        Task<List<Subscriber>> GetAllSubscribersAsync();

       
        Task<Subscriber?> GetSubscriberByIdAsync(int id);

       
        Task UpdateSubscriberAsync(Subscriber subscriber);

       
        Task DeleteSubscriberAsync(Subscriber subscriber);
    }
}
