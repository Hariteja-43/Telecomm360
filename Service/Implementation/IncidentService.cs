using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.Enum;
using Telecomm360.Models;
using Telecomm360.Repositories.Interface;
using Telecomm360.Services.Interface;

namespace Telecomm360.Services.Implementation
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<IEnumerable<IncidentResponse>> GetIncidentsAsync(SearchDto searchDto)
        {
            var list = await _incidentRepository.GetAllIncidentsAsync(searchDto);
            return list.Select(i => new IncidentResponse
            {
                DisplayId = "INC-" + i.IncidentID,
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
                AlarmID = long.Parse(invoiceDto.TargetAlarmId.Replace("ALM-", "")),
                AssignedTo = 101, // Simulated current authenticated user assignment
                Priority = System.Enum.Parse<PriorityEnum>(invoiceDto.IncidentPriority, true),
                Status = StatusEnum.Open
            };
            
            await _incidentRepository.AddIncidentAsync(incident);
            
            return new IncidentResponse
            {
                DisplayId = "INC-" + incident.IncidentID,
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
                DisplayId = "INC-" + incident.IncidentID,
                AssignedEngineer = "Eng_" + incident.AssignedTo,
                IncidentPriority = incident.Priority.ToString(),
                CurrentStatus = incident.Status.ToString(),
                ResolutionDetails = incident.ResolutionNotes
            };
        }
    }
}