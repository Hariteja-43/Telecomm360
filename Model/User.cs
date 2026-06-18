namespace Telecomm360.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RoleID { get; set; }
        public RoleEntity RoleEntity { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ResetToken { get; set; } = string.Empty;
    }
}