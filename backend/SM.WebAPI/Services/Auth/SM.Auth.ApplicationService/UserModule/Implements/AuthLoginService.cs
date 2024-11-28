using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SM.Auth.ApplicationService.UserModule.Abtracts;
using SM.Auth.Dtos.LoginModule;
using SM.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Google.Apis.Auth;
using System.Net.Http;
using System.Text.Json;
using SM.Auth.Dtos;

namespace SM.Auth.ApplicationService.UserModule.Implements
{
    public class AuthLoginService : IAuthLoginService
    {
        private readonly ILogger<AuthLoginService> _logger;
        private readonly AuthDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthLoginService(ILogger<AuthLoginService> logger, AuthDbContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        // Đăng nhập bằng tên người dùng và mật khẩu
        public async Task<AuthResponeDto> AuthLogin(LoginUserDto loginUserDto)
        {
            _logger.LogInformation("AuthLoginService.AuthLogin");

            var user = await _dbContext.AuthUsers
                .FirstOrDefaultAsync(x => x.username == loginUserDto.username && x.password == loginUserDto.password);

            if (user == null)
            {
                return new AuthResponeDto
                {
                    EM = "Invalid Username or Password",
                    EC = 1,
                    DT = ""
                };
            }

            string token = GenerateToken(user.name, user.userId);

            return new AuthResponeDto
            {
                EM = "Success",
                EC = 0,
                DT = token
            };
        }

        private string SecretKey => _configuration["Jwt:SecretKey"];

        private string GenerateToken(string name, int userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("name", name),
                new Claim("userId", userId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Tạo URL để đăng nhập Google
    //   public Task<string> GetUserEmailAsync(ClaimsPrincipal principal)
    //{
    //    return Task.FromResult(principal.FindFirst(ClaimTypes.Email)?.Value);
    //}

    //public Task<string> GetUserIdAsync(ClaimsPrincipal principal)
    //{
    //    return Task.FromResult(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    //}
    }

   

   
}
