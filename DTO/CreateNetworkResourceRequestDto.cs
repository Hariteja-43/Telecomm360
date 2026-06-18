namespace Telecomm360.DTO;

public class CreateNetworkResourceRequestDto
{
    public string NetworkResourceType { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public Status Status { get; set; }
}