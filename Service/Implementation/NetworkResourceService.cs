using Telecomm360.DTO;

using Telecomm360.Model;
using Telecomm360.Repository.Interface;
using Telecomm360.Service.Interface;

namespace Telecomm360.Service.Implementation
{
    public class NetworkResourceService : INetworkResourceService
    {
        private readonly INetworkResourceRepository _repository;

        public NetworkResourceService(INetworkResourceRepository repository)
        {
            _repository = repository;
        }

     
        public async Task<NetworkResourceResponseDto> CreateResourceAsync(CreateNetworkResourceRequestDto request)
        {
            var resource = new NetworkResource
            {
                Type = request.Type,
                Location = request.Location,
                Capacity = request.Capacity,
                Status = Status.Available,
                AllocatedTo = null
            };

            var created = await _repository.AddResourceAsync(resource);

            return MapToResponse(created);
        }

        
        public async Task<List<NetworkResourceResponseDto>> GetAllResourcesAsync()
        {
            var list = await _repository.GetAllResourcesAsync();

            return list.Select(r => MapToResponse(r)).ToList();
        }

       
        public async Task<NetworkResourceResponseDto?> GetResourceByIdAsync(int resourceId)
        {
            var r = await _repository.GetResourceByIdAsync(resourceId);

            if (r == null) return null;

            return MapToResponse(r);
        }

               public async Task<NetworkResourceResponseDto?> UpdateResourceAsync(int resourceId, CreateNetworkResourceRequestDto request)
        {
            var existing = await _repository.GetResourceByIdAsync(resourceId);

            if (existing == null) return null;

            existing.Type = request.Type;
            existing.Location = request.Location;
            existing.Capacity = request.Capacity;

            await _repository.UpdateResourceAsync(existing);

            return MapToResponse(existing);
        }

       
        public async Task<bool> DeleteResourceAsync(int resourceId)
        {
            return await _repository.DeleteResourceAsync(resourceId);
        }

        // ✅ Mapping Method
        private NetworkResourceResponseDto MapToResponse(NetworkResource r)
        {
            return new NetworkResourceResponseDto
            {
                ResourceId = r.ResourceId,
                Type = r.Type,
                Location = r.Location,
                Capacity = r.Capacity,
                AllocatedTo = r.AllocatedTo,
                Status = r.Status
            };
        }
    }
}