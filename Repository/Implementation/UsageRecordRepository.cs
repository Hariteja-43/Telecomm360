using Telecomm360.Data;
using Telecomm360.Model;
using Telecomm360.Repository.Interfaces;
using Telecomm360.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Telecomm360.Repository;

public class UsageRepository : IUsageRecordRepository
{
    private readonly AppDbContext _context;

    public UsageRepository(AppDbContext context)
    {
        _context = context;
    }

    //  Mapping Helper: Entity (Database) -> DTO (API)
    private static UsageRecordDto ToDto(UsageRecord r) => new UsageRecordDto
    {
        UsageRecordId = r.UsageRecordId,
        SubscriberID = r.SubscriberID,
        ServiceType = r.ServiceType,
        Quantity = r.Quantity,
        StartTime = r.StartTime,
        EndTime = r.EndTime
    };

    // Mapping Helper: DTO (API) -> Entity (Database)
    private static UsageRecord ToEntity(UsageRecordDto d) => new UsageRecord
    {
        UsageRecordId = d.UsageRecordId,
        SubscriberID = d.SubscriberID,
        ServiceType = d.ServiceType,
        Quantity = d.Quantity,
        StartTime = d.StartTime,
        EndTime = d.EndTime
    };

    //  FIXED: Replaced NotImplementedException with EF Core database call
    public List<UsageRecordDto> GetAllUsageRecord(UsageRecordDto dto)
    {
        return _context.UsageRecords
            .Select(r => ToDto(r))
            .ToList();
    }

    //  FIXED: Replaced NotImplementedException with EF Core database call
    public UsageRecordDto? GetUsageRecordById(UsageRecordDto usageRecordId)
    {
        var record = _context.UsageRecords.FirstOrDefault(u => u.UsageRecordId == usageRecordId.UsageRecordId);
        return record != null ? ToDto(record) : null;
    }

    //  FIXED: Replaced NotImplementedException with EF Core database call
    public List<UsageRecordDto> GetUsageRecordBySubscriber(UsageRecordDto subscriberId)
    {
        return _context.UsageRecords
            .Where(x => x.SubscriberID == subscriberId.SubscriberID)
            .Select(r => ToDto(r))
            .ToList();
    }

    //  FIXED: Replaced NotImplementedException with EF Core database call
    public UsageRecordDto CreateUsageRecord(UsageRecordDto usageRecordDto)
    {
        var entity = ToEntity(usageRecordDto);
        _context.UsageRecords.Add(entity);
        _context.SaveChanges();

        return ToDto(entity);
    }

    public List<UsageRecordDto> GetAllUsageRecord()
    {
        throw new NotImplementedException();
    }

  

    public List<UsageRecordDto> GetUsageRecordBySubscriber(string subscriberId)
    {
        throw new NotImplementedException();
    }
}