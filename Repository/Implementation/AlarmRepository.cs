using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Models;
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

        public async Task<IEnumerable<Alarm>> GetAllAlarmsAsync(SearchDto searchDto)
        {
            var query = _context.Alarms.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                // 🛠️ Updated to exactly match your Alarm.cs properties
                query = query.Where(a => 
                    a.AlarmID.ToString().Contains(searchDto.SearchTerm) ||
                    (a.Source != null && a.Source.Contains(searchDto.SearchTerm)) ||
                    a.Severity.ToString().Contains(searchDto.SearchTerm) ||
                    a.Status.ToString().Contains(searchDto.SearchTerm)
                );
            }
            
            return await query
                .Skip((searchDto.PageNumber - 1) * searchDto.PageSize)
                .Take(searchDto.PageSize)
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