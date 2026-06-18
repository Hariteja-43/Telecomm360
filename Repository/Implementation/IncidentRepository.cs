using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;

namespace Telecomm360.Repositories.Implementation
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly AppDbContext _context;

        public IncidentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Incident>> GetAllIncidentsAsync(SearchDtos searchDtos)
        {
            var query = _context.Incidents.AsQueryable();
            if (!string.IsNullOrEmpty(searchDtos.SearchTerm))
            {
                query = query.Where(i => i.ResolutionNotes.Contains(searchDtos.SearchTerm));
            }
            return await query.Skip((searchDtos.PageNumber - 1) * searchDtos.PageSize).Take(searchDtos.PageSize).ToListAsync();
        }

        public async Task<Incident> GetIncidentByIdAsync(long incidentId)
        {
            return await _context.Incidents.FindAsync(incidentId);
        }

        public async Task AddIncidentAsync(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIncidentAsync(Incident incident)
        {
            _context.Incidents.Update(incident);
            await _context.SaveChangesAsync();
        }
    }
}