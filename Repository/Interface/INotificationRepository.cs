using Telecomm360.DTO;
using Telecomm360.Model;

namespace Telecomm360.Repositories.Interface
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync(SearchDtos searchDtos);
        Task<Notification> GetNotificationByIdAsync(long notificationId);
        Task AddNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(Notification notification);
    }
}