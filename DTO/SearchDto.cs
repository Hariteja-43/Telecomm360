namespace Telecomm360.DTO;
public class SearchDto
{
    public string? SubscriberName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
