﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Dtos.LoginModule
{
    public class GoogleTokenResponse
    {
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
    }
}
