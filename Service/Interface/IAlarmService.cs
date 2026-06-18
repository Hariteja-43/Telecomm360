using Telecomm360.DTO;

namespace Telecomm360.Service.Interface
{
    public interface IAlarmService
    {
        Task<IEnumerable<AlarmResponse>> GetAlarmsAsync(SearchDtos searchDtos);
        Task<AlarmSummaryResponse> GetAlarmsSummaryAsync();
        Task<AlarmResponse> CreateAlarmAsync(AlarmCreateRequest invoiceDto);
    }
}