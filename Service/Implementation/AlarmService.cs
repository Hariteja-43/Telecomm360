using Telecomm360.DTO;
using Telecomm360.Enum;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;
using Telecomm360.Service.Interface;

namespace Telecomm360.Service.Implementation
{
    public class AlarmService : IAlarmService
    {
        // 🛠️ THE PERMANENT FIX: Added constructor injection for the repository and implemented all methods to handle alarms properly.
        private readonly IAlarmRepository _alarmRepository;

// 🛠️ THE PERMANENT FIX: Added constructor injection for the repository and implemented all methods to handle alarms properly.
        public AlarmService(IAlarmRepository alarmRepository)
        {
            
            _alarmRepository = alarmRepository;
        }

        public async Task<IEnumerable<AlarmResponse>> GetAlarmsAsync(SearchDtos searchDtos)
        {
            var list = await _alarmRepository.GetAllAlarmsAsync(searchDtos);
            return list.Select(a => new AlarmResponse
            {
                DisplayId = a.AlarmID,
                SourceNode = a.Source,
                FaultSeverity = a.Severity.ToString(),
                CurrentStatus = a.Status.ToString(),
                FormattedTimestamp = a.Timestamp
            });
        }

        public async Task<AlarmSummaryResponse> GetAlarmsSummaryAsync()
        {
            var list = await _alarmRepository.GetActiveAlarmsAsync();
            return new AlarmSummaryResponse
            {
                TotalCritical = list.Count(a => a.Severity == SeverityEnum.Critical),
                TotalMajor = list.Count(a => a.Severity == SeverityEnum.Major),
                TotalMinor = list.Count(a => a.Severity == SeverityEnum.Minor),
                TotalWarning = list.Count(a => a.Severity == SeverityEnum.Warning)
            };
        }

        public async Task<AlarmResponse> CreateAlarmAsync(AlarmCreateRequest invoiceDto)
        {
            var alarm = new Alarm
            {
                Source = invoiceDto.SourceNode,
                Severity = System.Enum.Parse<SeverityEnum>(invoiceDto.FaultSeverity, true),
                Status = StatusEnum.Open,
                Timestamp = DateTime.UtcNow
            };
            
            await _alarmRepository.AddAlarmAsync(alarm);
            
            return new AlarmResponse
            {
                DisplayId = alarm.AlarmID,
                SourceNode = alarm.Source,
                FaultSeverity = alarm.Severity.ToString(),
                CurrentStatus = alarm.Status.ToString(),
                FormattedTimestamp = alarm.Timestamp
            };
        }
    }
}