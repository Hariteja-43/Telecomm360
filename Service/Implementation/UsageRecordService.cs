using Telecomm360.DTOs;
using Telecomm360.Repository.Interfaces;
using Telecomm360.Service.Interface;
namespace Telecomm360.Service.Implementation
{   

public class UsageService : IUsageRecordService
{
    private readonly IUsageRecordRepository _repo;

    public UsageService(IUsageRecordRepository repo)
    {
        _repo = repo;
    }

    // Match SearchDto if the interface mandates it, or pass it if repository supports filtering
    public List<UsageRecordDto> GetAllUsageRecord(UsageRecordDto recordId)
        => _repo.GetAllUsageRecord(recordId);

    //  Cleaned up casing to match exactly: usageRecordId
    public UsageRecordDto? GetUsageRecordById(UsageRecordDto usageRecordId)
        => _repo.GetUsageRecordById(usageRecordId);

    //  Cleaned up casing to match exactly: subscriberId
    public List<UsageRecordDto> GetUsageRecordBySubscriber(UsageRecordDto subscriberId)
        => _repo.GetUsageRecordBySubscriber(subscriberId);

    //  Cleaned up casing to match exactly: usageRecordDto
    public UsageRecordDto CreateUsageRecord(UsageRecordDto usageRecordDto)
        => _repo.CreateUsageRecord(usageRecordDto);
}
}
