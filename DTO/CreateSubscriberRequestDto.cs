namespace Telecomm360.DTO;

public class CreateSubscriberRequestDto
{
    public int CustomerId { get; set; }
    public string MSISDN { get; set; } = string.Empty;
    public string IMSI { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;
}
