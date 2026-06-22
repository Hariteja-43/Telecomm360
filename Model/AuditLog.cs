using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecom360.Model;

namespace Telecomm360.Model
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuditLogID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string Resource { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; internal set; }
    }
}