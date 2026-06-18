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
                NetworkResourceType = request.NetworkResourceType,
                Location = request.Location,
                Capacity = request.Capacity,
                Status = Status.Available,
                AllocatedTo = 0
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

            existing.NetworkResourceType = request.NetworkResourceType ?? existing.NetworkResourceType;
            existing.Location = request.Location ?? existing.Location;
            existing.Capacity = request.Capacity != 0 ? request.Capacity : existing.Capacity;

            await _repository.UpdateResourceAsync(existing);

            return MapToResponse(existing);
        }

       
        public async Task<bool> DeleteResourceAsync(int networkResourceId)
        {
            return await _repository.DeleteResourceAsync(networkResourceId);
        }

        // ✅ Mapping Method
        private NetworkResourceResponseDto MapToResponse(NetworkResource r)
        {
            return new NetworkResourceResponseDto
            {
                NetworkResourceId = r.NetworkResourceId,
                NetworkResourceType = r.NetworkResourceType,
                Location = r.Location,
                Capacity = r.Capacity,
                AllocatedTo = r.AllocatedTo,
                Status = r.Status
            };
        }
    }
}