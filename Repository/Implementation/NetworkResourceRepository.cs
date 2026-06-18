using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.Model;
using Telecomm360.Repository.Interface;

namespace Telecomm360.Repository.Implementation
{
    public class NetworkResourceRepository : INetworkResourceRepository
    {
        private readonly AppDbContext _context;

        public NetworkResourceRepository(AppDbContext context)
        {
            _context = context;
        }

        // ✅ CREATE
        public async Task<NetworkResource> AddResourceAsync(NetworkResource resource)
        {
            await _context.NetworkResources.AddAsync(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        // ✅ GET ALL
        public async Task<List<NetworkResource>> GetAllResourcesAsync()
        {
            return await _context.NetworkResources.ToListAsync();
        }

        // ✅ GET BY ID
        public async Task<NetworkResource?> GetResourceByIdAsync(int resourceId)
        {
            return await _context.NetworkResources.FindAsync(resourceId);
        }

        // ✅ UPDATE (FIXED ✅)
        public async Task<bool> UpdateResourceAsync(NetworkResource resource)
        {
            var existing = await _context.NetworkResources
                                         .FindAsync(resource.NetworkResourceId);

            if (existing == null)
                return false;

            // ✅ CORRECT FIELDS
            existing.NetworkResourceType = resource.NetworkResourceType;
            existing.Location = resource.Location;
            existing.Capacity = resource.Capacity;
            existing.AllocatedTo = resource.AllocatedTo;
            existing.Status = resource.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ DELETE
        public async Task<bool> DeleteResourceAsync(int resourceId)
        {
            var resource = await _context.NetworkResources
                                         .FindAsync(resourceId);

            if (resource == null)
                return false;

            _context.NetworkResources.Remove(resource);
            await _context.SaveChangesAsync();

            return true;
        }

        Task INetworkResourceRepository.UpdateResourceAsync(NetworkResource resource)
        {
            return UpdateResourceAsync(resource);
        }
    }
}