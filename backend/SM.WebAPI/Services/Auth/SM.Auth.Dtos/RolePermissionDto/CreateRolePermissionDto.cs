using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Dtos.RolePermissionDto
{
    public class CreateRolePermissionDto
    {
        public int roleId { get; set; }
        public string permissionKey { get; set; }
    }
}
