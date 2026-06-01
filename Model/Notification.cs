using System;
using Telecomm360.Enum;

namespace Telecomm360.Models
{
    public class Notification
    {
        public long NotificationID { get; set; }
        public long CustomerID { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public NotificationStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}