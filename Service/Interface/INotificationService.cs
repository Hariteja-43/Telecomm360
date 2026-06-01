using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;
using Telecomm360.DTOs;

namespace Telecomm360.Services.Interface
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponse>> GetNotificationsAsync(SearchDto searchDto);
        Task<NotificationResponse> GetNotificationByIdAsync(long notificationId);
        Task<NotificationResponse> CreateNotificationAsync(NotificationCreateRequest invoiceDto);
        Task<NotificationResponse> UpdateNotificationAsync(long notificationId, NotificationUpdateRequest invoiceDto);
        Task<NotificationResponse> PatchNotificationStatusAsync(long notificationId, NotificationStatusPatchRequest invoiceDto);
        Task DeleteNotificationAsync(long notificationId);
    }
}