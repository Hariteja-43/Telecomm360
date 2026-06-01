namespace Telecomm360.DTO;

public class CreateCustomerRequestDto
{

    
    public required string Name { get; set; } = string.Empty;

    public required string Type { get; set; } = string.Empty;

    public required string KYCStatus { get; set; } = string.Empty;

    public required string ContactInfo { get; set; } = string.Empty;
}