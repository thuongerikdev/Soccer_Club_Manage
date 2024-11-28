using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Dtos.LoginModule
{
    public class GoogleAuthResult
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
