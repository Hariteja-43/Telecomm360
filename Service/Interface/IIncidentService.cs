using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;

namespace Telecomm360.Service.Interface
{
    public interface IIncidentService
    {
        Task<IEnumerable<IncidentResponse>> GetIncidentsAsync(SearchDtos searchDtos);
        Task<IncidentResponse> CreateIncidentAsync(IncidentCreateRequest invoiceDto);
        Task<IncidentResponse> PatchIncidentAsync(long incidentId, IncidentPatchRequest invoiceDto);
    }
}