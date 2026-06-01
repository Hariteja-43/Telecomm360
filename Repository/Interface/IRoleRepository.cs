using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;
using Telecomm360.Models;

namespace Telecomm360.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleEntity>> GetAllRolesAsync(SearchDto searchDto);
        Task<RoleEntity> GetRoleByIdAsync(long roleId);
        Task AddRoleAsync(RoleEntity role);
        Task UpdateRoleAsync(RoleEntity role);
        Task DeleteRoleAsync(RoleEntity role);
    }
}