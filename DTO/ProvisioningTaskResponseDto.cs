// using Telecomm360.Enum;

namespace Telecomm360.DTO;

public class ProvisioningTaskResponseDto
{
    public int ProvisioningTaskId { get; set; }

    public int OrderId { get; set; } 

    public int SubscriberId { get; set; }

    public string MSISDN { get; set; } = string.Empty;

    public string ResourceType { get; set; } = string.Empty;

    //public string TargetSubsystem { get; set; } = string.Empty;

    public Status Status { get; set; }
}