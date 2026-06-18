using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;

namespace Telecomm360.Repositories.Implementation
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly AppDbContext _context;

        public AlarmRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alarm>> GetAllAlarmsAsync(SearchDtos searchDtos)
        {
            var query = _context.Alarms.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchDtos.SearchTerm))
            {
                // 🛠️ Updated to exactly match your Alarm.cs properties
                query = query.Where(a => 
                    a.AlarmID.ToString().Contains(searchDtos.SearchTerm) ||
                    (a.Source != null && a.Source.Contains(searchDtos.SearchTerm)) ||
                    a.Severity.ToString().Contains(searchDtos.SearchTerm) ||
                    a.Status.ToString().Contains(searchDtos.SearchTerm)
                );
            }
            
            return await query
                .Skip((searchDtos.PageNumber - 1) * searchDtos.PageSize)
                .Take(searchDtos.PageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alarm>> GetActiveAlarmsAsync()
        {
            return await _context.Alarms.ToListAsync();
        }

        public async Task AddAlarmAsync(Alarm alarm)
        {
            await _context.Alarms.AddAsync(alarm);
            await _context.SaveChangesAsync();
        }
    }
}