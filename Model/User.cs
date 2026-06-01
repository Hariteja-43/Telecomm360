namespace Telecomm360.Models
{
    public class User
    {
        public long UserID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        
        // 🛠️ THE PERMANENT FIX: Defends against the SQL NULL crash!
        public string ResetToken { get; set; } = string.Empty;
    }
}