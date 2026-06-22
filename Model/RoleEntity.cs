using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telecomm360.Enum;

namespace Telecomm360.Model
{
    public class RoleEntity
    {
    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public RoleStatusEnum Status { get; set; }
    }
}