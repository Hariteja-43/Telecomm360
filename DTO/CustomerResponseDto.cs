namespace Telecomm360.DTO;

public class CustomerResponseDto
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string KYCStatus { get; set; } = string.Empty;

    public string ContactInfo { get; set; } = string.Empty;
}
