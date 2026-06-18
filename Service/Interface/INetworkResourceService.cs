using Telecomm360.DTO;

namespace Telecomm360.Service.Interface
{
    public interface INetworkResourceService
    {
        Task<NetworkResourceResponseDto> CreateResourceAsync(CreateNetworkResourceRequestDto request);

        Task<List<NetworkResourceResponseDto>> GetAllResourcesAsync();

        Task<NetworkResourceResponseDto?> GetResourceByIdAsync(int resourceId);

        Task<NetworkResourceResponseDto?> UpdateResourceAsync(int resourceId, CreateNetworkResourceRequestDto request);

        Task<bool> DeleteResourceAsync(int resourceId);
    }
}
