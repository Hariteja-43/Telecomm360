using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Enum;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;
using Telecomm360.Service.Interface;

namespace Telecomm360.Service.Implementation
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<IEnumerable<IncidentResponse>> GetIncidentsAsync(SearchDtos searchDtos)
        {
            var list = await _incidentRepository.GetAllIncidentsAsync(searchDtos);
            return list.Select(i => new IncidentResponse
            {
                DisplayId = i.IncidentID,
                AssignedEngineer = "Eng_" + i.AssignedTo,
                IncidentPriority = i.Priority.ToString(),
                CurrentStatus = i.Status.ToString(),
                ResolutionDetails = i.ResolutionNotes
            });
        }

        public async Task<IncidentResponse> CreateIncidentAsync(IncidentCreateRequest invoiceDto)
        {
            var incident = new Incident
            {
                AlarmID = invoiceDto.TargetAlarmId,
                AssignedTo = 101, // Simulated current authenticated user assignment
                Priority = System.Enum.Parse<PriorityEnum>(invoiceDto.IncidentPriority, true),
                Status = StatusEnum.Open
            };
            
            await _incidentRepository.AddIncidentAsync(incident);
            
            return new IncidentResponse
            {
                DisplayId = incident.IncidentID,
                AssignedEngineer = "Eng_" + incident.AssignedTo,
                IncidentPriority = incident.Priority.ToString(),
                CurrentStatus = incident.Status.ToString(),
                ResolutionDetails = incident.ResolutionNotes
            };
        }

        public async Task<IncidentResponse> PatchIncidentAsync(long incidentId, IncidentPatchRequest invoiceDto)
        {
            var incident = await _incidentRepository.GetIncidentByIdAsync(incidentId);
            if (incident == null) throw new KeyNotFoundException(FaultMessageConstants.IncidentNotFound);

            if (!string.IsNullOrEmpty(invoiceDto.UpdatedStatus))
            {
                incident.Status = System.Enum.Parse<StatusEnum>(invoiceDto.UpdatedStatus, true);
            }
            if (!string.IsNullOrEmpty(invoiceDto.ResolutionDetails))
            {
                incident.ResolutionNotes = invoiceDto.ResolutionDetails;
            }

            await _incidentRepository.UpdateIncidentAsync(incident);

            return new IncidentResponse
            {
                DisplayId = incident.IncidentID,
                AssignedEngineer = "Eng_" + incident.AssignedTo,
                IncidentPriority = incident.Priority.ToString(),
                CurrentStatus = incident.Status.ToString(),
                ResolutionDetails = incident.ResolutionNotes
            };
        }
    }
}