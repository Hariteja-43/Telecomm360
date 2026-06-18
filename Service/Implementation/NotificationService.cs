using Telecomm360.Constants;
using Telecomm360.DTOs;
using Telecomm360.DTO;
using Telecomm360.Enum;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;
using Telecomm360.Service.Interface;

namespace Telecomm360.Service.Implementation
{
    
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<NotificationResponse>> GetNotificationsAsync(SearchDtos searchDtos)
        {
            var list = await _notificationRepository.GetAllNotificationsAsync(searchDtos);
            return list.Select(n => new NotificationResponse
            {
                DisplayId = "NOTIF-" + n.NotificationID,
                TargetSubscriberLabel = "Sub_" + n.CustomerID,
                DeliveryChannel = n.Channel,
                MessageContent = n.Message,
                CurrentStatus = n.Status.ToString(),
                FormattedTimestamp = n.CreatedDate
            });
        }

        public async Task<NotificationResponse> GetNotificationByIdAsync(long notificationId)
        {
            var n = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (n == null) throw new KeyNotFoundException(MessageConstants.NotificationNotFound);
            return new NotificationResponse
            {
                DisplayId = "NOTIF-" + n.NotificationID,
                TargetSubscriberLabel = "Sub_" + n.CustomerID,
                DeliveryChannel = n.Channel,
                MessageContent = n.Message,
                CurrentStatus = n.Status.ToString(),
                FormattedTimestamp = n.CreatedDate
            };
        }

        public async Task<NotificationResponse> CreateNotificationAsync(NotificationCreateRequest invoiceDto)
        {
            var n = new Notification
            {
                SubscriberID = 101,
                Channel = invoiceDto.DeliveryChannel,
                Message = invoiceDto.MessageContent,
                Status = NotificationStatusEnum.Pending,
                CreatedDate = DateTime.UtcNow
            };
            await _notificationRepository.AddNotificationAsync(n);
            return new NotificationResponse
            {
                NotificationID = n.NotificationID,
                CustomerID = n.CustomerID,
                DeliveryChannel = n.Channel,
                MessageContent = n.Message,
                CurrentStatus = n.Status.ToString(),
                FormattedTimestamp = n.CreatedDate
            };
        }

        public async Task<NotificationResponse> UpdateNotificationAsync(long notificationId, NotificationUpdateRequest invoiceDto)
        {
            var n = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (n == null) throw new KeyNotFoundException(MessageConstants.NotificationNotFound);
            n.Channel = invoiceDto.DeliveryChannel;
            n.Message = invoiceDto.MessageContent;
            await _notificationRepository.UpdateNotificationAsync(n);
            return new NotificationResponse
            {
                DisplayId = "NOTIF-" + n.NotificationID,
                TargetSubscriberLabel = "Sub_" + n.CustomerID,
                DeliveryChannel = n.Channel,
                MessageContent = n.Message,
                CurrentStatus = n.Status.ToString(),
                FormattedTimestamp = n.CreatedDate
            };
        }

        public async Task<NotificationResponse> PatchNotificationStatusAsync(long notificationId, NotificationStatusPatchRequest invoiceDto)
        {
            var n = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (n == null) throw new KeyNotFoundException(MessageConstants.NotificationNotFound);
            
           
            n.Status = System.Enum.Parse<NotificationStatusEnum>(invoiceDto.UpdatedStatus, true);
            
            await _notificationRepository.UpdateNotificationAsync(n);
            return new NotificationResponse
            {
                DisplayId = "NOTIF-" + n.NotificationID,
                TargetSubscriberLabel = "Sub_" + n.CustomerID,
                DeliveryChannel = n.Channel,
                MessageContent = n.Message,
                CurrentStatus = n.Status.ToString(),
                FormattedTimestamp = n.CreatedDate
            };
        }

        public async Task DeleteNotificationAsync(long notificationId)
        {
            var n = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (n == null) throw new KeyNotFoundException(MessageConstants.NotificationNotFound);
            await _notificationRepository.DeleteNotificationAsync(n);
        }
    }
}