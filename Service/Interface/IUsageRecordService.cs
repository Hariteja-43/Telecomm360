using System.Collections.Generic;
using Telecomm360.DTOs;

namespace Telecomm360.Repository.Interfaces;

public interface IUsageRecordService
{
    List<UsageRecordDto> GetAllUsageRecord(UsageRecordDto recordId);
    UsageRecordDto? GetUsageRecordById(UsageRecordDto usageRecordId); // Must be int usageRecordId
    List<UsageRecordDto> GetUsageRecordBySubscriber(UsageRecordDto subscriberId); // Must be string subscriberId
    UsageRecordDto CreateUsageRecord(UsageRecordDto usageRecordDto);
    
}