﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Dtos.LoginModule
{
    public  class AuthLoginResultDto
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public int phone { get; set; }
        public string name { get; set; }
        public string token { get; set; }
    }
}
