// using Telecomm360.Enum;

namespace Telecomm360.DTO;

public class CreateNetworkResourceRequestDto
{
    public  required string Type { get; set; } = string.Empty;

    public required string Location { get; set; } = string.Empty;

    public required int Capacity { get; set; }

    public required Status Status { get; set; }
}