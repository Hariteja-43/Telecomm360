namespace Telecomm360.DTOs
{
    public class RoleCreateRequest
    {
        public required string RoleName { get; set; }
        public required string ConfigurationDescription { get; set; }
    }

    public class RoleUpdateRequest
    {
        public required  string RoleName { get; set; }
        public string ConfigurationDescription { get; set; }
    }

    public class RoleStatusPatchRequest
    {
        public string SystemStatus { get; set; }
    }

    public class RoleResponse
    {
        public string DisplayId { get; set; }
        public string RoleName { get; set; }
        public string ConfigurationDescription { get; set; }
        public string SystemStatus { get; set; }
    }
}