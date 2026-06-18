using Telecomm360.Model;

namespace Telecomm360.Repository.Interface
{
    public interface INetworkResourceRepository
    {
        Task<NetworkResource> AddResourceAsync(NetworkResource networkResource);

        Task<List<NetworkResource>> GetAllResourcesAsync();

        Task<NetworkResource?> GetResourceByIdAsync(int networkResourceId);

        Task UpdateResourceAsync(NetworkResource networkResource);

        Task<bool> DeleteResourceAsync(int networkResourceId);
    }
}