// using Telecomm360.Enum;

namespace Telecomm360.DTO;

public class SubscriberResponseDto
{
    public long SubscriberId { get; set; }

    public int CustomerId { get; set; }

    public string MSISDN { get; set; } = string.Empty;

    public string IMSI { get; set; } = string.Empty;

    public Status SIMStatus { get; set; }

    public string DeviceId { get; set; } = string.Empty;

    public Status Status { get; set; }
}