// using Telecomm360.Enum;

namespace Telecomm360.DTO;

public class CreateSubscriberRequestDto
{
    public required int CustomerId { get; set; }

    public required string MSISDN { get; set; } = string.Empty;

    public required string IMSI { get; set; } = string.Empty;
    //public Status SIMStatus { get; set; }

    public required string DeviceId { get; set; } = string.Empty;
}
