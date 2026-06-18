using System;

namespace Telecomm360.DTOs
{
    public class NotificationCreateRequest
    {
        public required string DeliveryChannel { get; set; }
        public required string MessageContent { get; set; }
    }

    public class NotificationUpdateRequest
    {
        public required string DeliveryChannel { get; set; }
        public required string MessageContent { get; set; }
    }

    public class NotificationStatusPatchRequest
    {
        public required string UpdatedStatus { get; set; }
    }

    public class NotificationResponse
    {
        public required string DisplayId { get; set; }
        public required string TargetSubscriberLabel { get; set; }
        public required string DeliveryChannel { get; set; }
        public required string MessageContent { get; set; }
        public required string CurrentStatus { get; set; }
        public DateTime FormattedTimestamp { get; set; }
    }
}