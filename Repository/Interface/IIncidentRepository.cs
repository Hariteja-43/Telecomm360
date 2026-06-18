using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;
using Telecomm360.Model;

namespace Telecomm360.Repositories.Interface
{
    public interface IIncidentRepository
    {
        Task<IEnumerable<Incident>> GetAllIncidentsAsync(SearchDtos searchDtos);
        Task<Incident> GetIncidentByIdAsync(long incidentId);
        Task AddIncidentAsync(Incident incident);
        Task UpdateIncidentAsync(Incident incident);
    }
}