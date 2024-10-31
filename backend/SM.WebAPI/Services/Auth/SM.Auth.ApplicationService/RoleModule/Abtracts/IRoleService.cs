using SM.Auth.Dtos;
using SM.Auth.Dtos.AuthRoleDto;
using SM.Auth.Dtos.LoginModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.ApplicationService.RoleModule.Abtracts
{
    public interface IRoleService
    {
        public Task<AuthResponeDto> CreateRole(CreateRoleDto createroleDto);
        public Task<AuthResponeDto> UpdateRole( int roleId , UpdateRoleDto updateRoleDto);
        public Task<AuthResponeDto> DeleteRole(int roleId);
        public Task<AuthResponeDto> GetRole(int roleId);
        public Task<AuthResponeDto> GetAllRoles();
    }
}
