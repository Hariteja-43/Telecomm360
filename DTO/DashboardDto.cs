namespace Telecomm360.DTO
{
    public class DashboardDto
    {
        public int Subscribers { get; set; }
        public decimal ARPU { get; set; }
        public decimal ChurnRate { get; set; }
        public string KPIs { get; set; } = string.Empty;
    }
}