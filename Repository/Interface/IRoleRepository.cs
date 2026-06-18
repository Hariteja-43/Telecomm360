using Telecomm360.Model;
using Telecomm360.DTO;
 
namespace Telecomm360.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleEntity>> GetAllRolesAsync(SearchDtos searchDtos);
        Task<RoleEntity> GetRoleByIdAsync(long roleId);
        Task<RoleEntity> GetRoleByNameAsync(string roleName);
        Task AddRoleAsync(RoleEntity role);
        Task UpdateRoleAsync(RoleEntity role);
        Task DeleteRoleAsync(RoleEntity role);
    }
}