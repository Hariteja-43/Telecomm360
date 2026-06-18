namespace Telecomm360.DTO;

public class CreateProvisioningTaskRequestDto
{
    public required string OrderId { get; set; } = string.Empty;

    public required int SubscriberId { get; set; }

    public required string MSISDN { get; set; } = string.Empty;
    public required string ResourceType { get; set; } = string.Empty;

   // public string TargetSubsystem { get; set; } = string.Empty;
}
