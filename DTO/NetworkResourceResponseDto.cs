
namespace Telecomm360.DTO;

public class NetworkResourceResponseDto
{
    public int ResourceId { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public int? AllocatedTo { get; set; }

    public Status Status { get; set; }
}
