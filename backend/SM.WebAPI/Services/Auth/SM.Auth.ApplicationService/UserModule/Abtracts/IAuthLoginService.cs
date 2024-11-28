using SM.Auth.Dtos;
using SM.Auth.Dtos.LoginModule;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SM.Auth.ApplicationService.UserModule.Abtracts
{
    public interface IAuthLoginService
    {
        Task<AuthResponeDto> AuthLogin(LoginUserDto loginUserDto);
        //Task<string> GetGoogleLoginUrlAsync();
        //Task<string> HandleGoogleCallbackAsync(string code);
    }
}
