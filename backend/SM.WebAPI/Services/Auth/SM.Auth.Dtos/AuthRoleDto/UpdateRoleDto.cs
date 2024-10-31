﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Dtos.AuthRoleDto
{
    public class UpdateRoleDto
    {
        public int RoleId { get; set; }
        public string roleName { get; set; }
        public string description { get; set; }
        public string roleType { get; set; }
        public int roleCode { get; set; }
        public DateTime Created { get; set; }
        public string status { get; set; }
    }
}
