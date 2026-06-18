using Telecomm360.DTO;
using Telecomm360.DTOs;

namespace Telecomm360.Service.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetRolesAsync(SearchDtos searchDtos);
        Task<RoleResponse> CreateRoleAsync(RoleCreateRequest invoiceDto);
        Task<RoleResponse> UpdateRoleAsync(long roleId, RoleUpdateRequest invoiceDto);
        Task<RoleResponse> PatchRoleStatusAsync(long roleId, RoleStatusPatchRequest invoiceDto);
        Task DeleteRoleAsync(long roleId);
    }
}