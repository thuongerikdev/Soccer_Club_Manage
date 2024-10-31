using SM.Auth.Dtos;
using SM.Auth.Dtos.LoginModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.ApplicationService.UserModule.Abtracts
{
    public interface IAuthLoginService
    {
        public Task<AuthResponeDto> AuthLogin(LoginUserDto loginUserDto);
    }
}
