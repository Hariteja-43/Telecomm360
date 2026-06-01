using System.Collections.Generic;
using Telecomm360.DTOs;

namespace Telecomm360.Repository.Interfaces
{
    public interface IUsageRecordRepository
    {
        List<UsageRecordDto> GetAllUsageRecord(UsageRecordDto dto);
        UsageRecordDto? GetUsageRecordById(UsageRecordDto UsageRecordId);
        List<UsageRecordDto> GetUsageRecordBySubscriber(UsageRecordDto subscriberId);
        UsageRecordDto CreateUsageRecord(UsageRecordDto usageRecorddto);
    }
}