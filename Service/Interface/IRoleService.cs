using System.Collections.Generic;
using System.Threading.Tasks;
using Telecomm360.DTO;
using Telecomm360.DTOs;

namespace Telecomm360.Services.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetRolesAsync(SearchDto searchDto);
        Task<RoleResponse> CreateRoleAsync(RoleCreateRequest invoiceDto);
        Task<RoleResponse> UpdateRoleAsync(long roleId, RoleUpdateRequest invoiceDto);
        Task<RoleResponse> PatchRoleStatusAsync(long roleId, RoleStatusPatchRequest invoiceDto);
        Task DeleteRoleAsync(long roleId);
    }
}