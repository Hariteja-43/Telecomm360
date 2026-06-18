using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telecomm360.Constants;
using Telecomm360.DTO;
using Telecomm360.DTOs;
using Telecomm360.Enum;
using Telecomm360.Models;
using Telecomm360.Repositories.Interface;
using Telecomm360.Services.Interface;

namespace Telecomm360.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleResponse>> GetRolesAsync(SearchDto searchDto)
        {
            var list = await _roleRepository.GetAllRolesAsync(searchDto);
            return list.Select(r => new RoleResponse
            {
                DisplayId = "ROLE-" + r.RoleID,
                RoleName = r.Name,
                ConfigurationDescription = r.Description,
                SystemStatus = r.Status.ToString()
            });
        }

        public async Task<RoleResponse> CreateRoleAsync(RoleCreateRequest invoiceDto)
        {
            var r = new RoleEntity
            {
                Name = invoiceDto.RoleName,
                Description = invoiceDto.ConfigurationDescription,
                Status = RoleStatusEnum.Active
            };
            await _roleRepository.AddRoleAsync(r);
            return new RoleResponse
            {
                DisplayId = "ROLE-" + r.RoleID,
                RoleName = r.Name,
                ConfigurationDescription = r.Description,
                SystemStatus = r.Status.ToString()
            };
        }

        public async Task<RoleResponse> UpdateRoleAsync(long roleId, RoleUpdateRequest invoiceDto)
        {
            var r = await _roleRepository.GetRoleByIdAsync(roleId);
            if (r == null) throw new KeyNotFoundException(MessageConstants.RoleNotFound);
            r.Name = invoiceDto.RoleName;
            r.Description = invoiceDto.ConfigurationDescription;
            await _roleRepository.UpdateRoleAsync(r);
            return new RoleResponse
            {
                DisplayId = "ROLE-" + r.RoleID,
                RoleName = r.Name,
                ConfigurationDescription = r.Description,
                SystemStatus = r.Status.ToString()
            };
        }

        public async Task<RoleResponse> PatchRoleStatusAsync(long roleId, RoleStatusPatchRequest invoiceDto)
        {
            var r = await _roleRepository.GetRoleByIdAsync(roleId);
            if (r == null) throw new KeyNotFoundException(MessageConstants.RoleNotFound);
            
            // Explicitly use System.Enum to fix the error
            r.Status = System.Enum.Parse<RoleStatusEnum>(invoiceDto.SystemStatus, true);
            
            await _roleRepository.UpdateRoleAsync(r);
            return new RoleResponse
            {
                DisplayId = "ROLE-" + r.RoleID,
                RoleName = r.Name,
                ConfigurationDescription = r.Description,
                SystemStatus = r.Status.ToString()
            };
        }

        public async Task DeleteRoleAsync(long roleId)
        {
            var r = await _roleRepository.GetRoleByIdAsync(roleId);
            if (r == null) throw new KeyNotFoundException(MessageConstants.RoleNotFound);
            await _roleRepository.DeleteRoleAsync(r);
        }
    }
}