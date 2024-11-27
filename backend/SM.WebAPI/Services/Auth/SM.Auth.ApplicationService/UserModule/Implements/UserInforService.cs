using Microsoft.Extensions.Logging;
using SM.Auth.ApplicationService.Common;
using SM.Auth.ApplicationService.RoleModule.Abtracts;
using SM.Auth.Dtos;
using SM.Auth.Infrastructure;
using SM.Shared.ApplicationService.Dto;
using SM.Shared.ApplicationService.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.ApplicationService.UserModule.Implements
{
    public class UserInforService : AuthServiceBase, IUserInforSerivce
    {
        public UserInforService(ILogger<UserInforService> logger, AuthDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<getUserDto> GetUserInforAsync(int userId)
        {
            var user =  _dbContext.AuthUsers.FirstOrDefault(x => x.userId ==userId );
            if (user == null)
            {
                return null;
            }
            var userDto = new getUserDto
            {
                userId = user.userId,
                username = user.username,
                password = user.password,
                email = user.email,
                age = user.age,
                address = user.address,
                gender = user.gender,
                phone = user.phone,
                name = user.name
            };

            return userDto;

        }
    }
}
