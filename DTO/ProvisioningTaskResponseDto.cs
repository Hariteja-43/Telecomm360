// using Telecomm360.Enum;

namespace Telecomm360.DTO;

public class ProvisioningTaskResponseDto
{
    public int TaskId { get; set; }

    public string OrderId { get; set; } = string.Empty;

    public int SubscriberId { get; set; }

    public string MSISDN { get; set; } = string.Empty;

    public string ResourceType { get; set; } = string.Empty;

    //public string TargetSubsystem { get; set; } = string.Empty;

    public Status Status { get; set; }
}