
namespace Telecomm360.DTO;

public class UpdateSubscriberRequestDto
{
    public required int CustomerId { get; set; }

    public required string MSISDN { get; set; } = string.Empty;

    public required string IMSI { get; set; } = string.Empty;
    //public Status SIMStatus { get; set; }

    public required string DeviceId { get; set; } = string.Empty;
}