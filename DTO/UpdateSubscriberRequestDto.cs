namespace Telecomm360.DTO;

public class UpdateSubscriberRequestDto
{
    public int? CustomerId { get; set; }
    public string? MSISDN { get; set; }
    public string? IMSI { get; set; }
    public string? DeviceId { get; set; }
}