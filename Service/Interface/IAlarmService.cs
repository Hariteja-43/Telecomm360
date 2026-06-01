using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;

namespace Telecomm360.Services.Interface
{
    public interface IAlarmService
    {
        Task<IEnumerable<AlarmResponse>> GetAlarmsAsync(SearchDto searchDto);
        Task<AlarmSummaryResponse> GetAlarmsSummaryAsync();
        Task<AlarmResponse> CreateAlarmAsync(AlarmCreateRequest invoiceDto);
    }
}