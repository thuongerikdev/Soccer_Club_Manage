﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Dtos.RolePermissionDto
{
    public  class UpdateRolePermissionDto
    {
        public int rolePermissionId { get; set; }
        public int roleId { get; set; }
        public string permissionKey { get; set; }
    }
}
