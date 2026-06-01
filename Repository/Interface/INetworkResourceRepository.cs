using Telecomm360.Model;

namespace Telecomm360.Repository.Interface
{
    public interface INetworkResourceRepository
    {
        Task<NetworkResource> AddResourceAsync(NetworkResource resource);

        Task<List<NetworkResource>> GetAllResourcesAsync();

        Task<NetworkResource?> GetResourceByIdAsync(int resourceId);

        Task UpdateResourceAsync(NetworkResource resource);

        Task<bool> DeleteResourceAsync(int resourceId);
    }
}