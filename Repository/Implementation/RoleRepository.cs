using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Telecomm360.Data;
using Telecomm360.DTO;
using Telecomm360.Models;
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

        public async Task<IEnumerable<RoleEntity>> GetAllRolesAsync(SearchDto searchDto)
        {
            var query = _context.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchDto.SearchTerm))
            {
                query = query.Where(r => r.Name.Contains(searchDto.SearchTerm) || r.Description.Contains(searchDto.SearchTerm));
            }
            return await query.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize).ToListAsync();
        }

        public async Task<RoleEntity> GetRoleByIdAsync(long roleId)
        {
            return await _context.Roles.FindAsync(roleId);
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