using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SM.Auth.ApplicationService.Common;
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

namespace SM.Auth.ApplicationService.UserModule.Implements
{
    public class AuthLoginService : AuthServiceBase, IAuthLoginService
    {
        private readonly IConfiguration _configuration;

        public AuthLoginService(ILogger<AuthService> logger, AuthDbContext dbContext, IConfiguration configuration) : base(logger, dbContext)
        {
            _configuration = configuration;
        }
        public async Task<AuthResponeDto> AuthLogin(LoginUserDto loginUserDto)
        {
            _logger.LogInformation("AuthLoginService.AuthLogin");

            // Fetch the user from the database by username
            var user = await _dbContext.AuthUsers
                .FirstOrDefaultAsync(x => x.username == loginUserDto.username);

            // Check if the user exists and validate the password
            if (user == null)
            {
                return new AuthResponeDto
                {
                    EM = "Invalid Username or password ",
                    EC = 1,
                    DT = ""
                };
            }

            string? token = GenerateToken(user.name, user.userId);

            return new AuthResponeDto
            {
                EM = "success",
                EC = 0,
                DT = token 
            }; 
        }
        private string SecretKey => _configuration["Jwt:SecretKey"];

        private string GenerateToken(string name , int userId)
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

    }
}
