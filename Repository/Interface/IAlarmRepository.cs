using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;
using Telecomm360.Models;

namespace Telecomm360.Repositories.Interface
{
    public interface IAlarmRepository
    {
        Task<IEnumerable<Alarm>> GetAllAlarmsAsync(SearchDto searchDto);
        Task<IEnumerable<Alarm>> GetActiveAlarmsAsync();
        Task AddAlarmAsync(Alarm alarm);
    }
}