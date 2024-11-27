using SM.Auth.Dtos;
using SM.Shared.ApplicationService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Shared.ApplicationService.User
{
    public interface IUserInforSerivce
    {
        public Task<getUserDto> GetUserInforAsync(int userId);
    }
}
