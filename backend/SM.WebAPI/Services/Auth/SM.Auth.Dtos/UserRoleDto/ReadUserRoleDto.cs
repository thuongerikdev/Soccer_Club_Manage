﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Dtos.UserRoleDto
{
    public class ReadUserRoleDto
    {
        public int userRoleId { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }
    }
}
