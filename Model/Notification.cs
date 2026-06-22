using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecomm360.Enum;

namespace Telecomm360.Model
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationID { get; set; }
        public int SubscriberID { get; set; }
        public Subscriber Subscriber {get;set;}
        public string Channel { get; set; }
        public string Message { get; set; }
        public NotificationStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public  long CustomerID { get; internal set; }
    }
}