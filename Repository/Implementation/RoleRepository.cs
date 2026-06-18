using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Model;
using Telecomm360.Repositories.Interface;
 
namespace Telecomm360.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
 
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
 
        public async Task<IEnumerable<RoleEntity>> GetAllRolesAsync(SearchDtos searchDtos)
        {
            var query = _context.Roles.AsQueryable();
 
            if (!string.IsNullOrEmpty(searchDtos.SearchTerm))
            {
                query = query.Where(r =>
                    r.Name.Contains(searchDtos.SearchTerm) ||
                    r.Description.Contains(searchDtos.SearchTerm));
            }
 
            return await query
                .Skip((searchDtos.PageNumber - 1) * searchDtos.PageSize)
                .Take(searchDtos.PageSize)
                .ToListAsync();
        }
 
        public async Task<RoleEntity> GetRoleByIdAsync(long roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }
 
        // ✅ ADD THIS METHOD
        public async Task<RoleEntity> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName);
        }
 
        public async Task AddRoleAsync(RoleEntity role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }
 
        public async Task UpdateRoleAsync(RoleEntity role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
 
        public async Task DeleteRoleAsync(RoleEntity role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
 