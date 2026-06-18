using Telecomm360.DTO;
using Telecomm360.Model;

namespace Telecomm360.Repositories.Interface
{
    public interface IAlarmRepository
    {
        Task<IEnumerable<Alarm>> GetAllAlarmsAsync(SearchDtos searchDtos);
        Task<IEnumerable<Alarm>> GetActiveAlarmsAsync();
        Task AddAlarmAsync(Alarm alarm);
    }
}