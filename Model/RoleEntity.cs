using Telecomm360.Enum;

namespace Telecomm360.Models
{
    public class RoleEntity
    {
    
        public long RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleStatusEnum Status { get; set; }
    }
}