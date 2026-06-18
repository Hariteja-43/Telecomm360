
namespace Telecomm360.DTO;

public class NetworkResourceResponseDto
{
    public int NetworkResourceId { get; set; }

    public string NetworkResourceType { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public int? AllocatedTo { get; set; }

    public Status Status { get; set; }
}
