namespace Telecomm360.DTO;

public class CreateProvisioningTaskRequestDto
{
    public int OrderId { get; set; } 
    public int SubscriberId { get; set; }
    public string MSISDN { get; set; } = string.Empty;
    public string ResourceType { get; set; } = string.Empty;
}
