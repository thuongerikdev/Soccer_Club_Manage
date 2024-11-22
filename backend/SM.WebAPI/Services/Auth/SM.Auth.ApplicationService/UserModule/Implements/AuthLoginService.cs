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
using SM.Auth.Dtos;
using SM.Auth.Domain;
using Google.Apis.Auth;

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
                .FirstOrDefaultAsync(x => x.username == loginUserDto.username && x.password == loginUserDto.password);

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
        public async Task<AuthResponeDto> AuthLoginWithGoogle(string googleToken)
        {
            _logger.LogInformation("AuthLoginService.AuthLoginWithGoogle");

            try
            {
                // Xác thực Google Token
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["Google:ClientId"] }
                });

                // Kiểm tra xem người dùng đã tồn tại trong hệ thống chưa
                var user = await _dbContext.AuthUsers.FirstOrDefaultAsync(x => x.email == payload.Email);

                if (user == null)
                {
                    // Nếu người dùng chưa tồn tại, tạo mới
                    user = new AuthUser
                    {
                        name = payload.Name,
                        email = payload.Email,
                        age = 0,
                        address = "",
                        gender = "",
                        phone = 0,
                        password="",
                        username = "",
                        
                        // Có thể thêm các trường khác như ảnh đại diện, giới tính, v.v.
                    };

                    _dbContext.AuthUsers.Add(user);
                    await _dbContext.SaveChangesAsync();
                }

                // Sinh token JWT
                string token = GenerateToken(user.name, user.userId);

                return new AuthResponeDto
                {
                    EM = "Login with Google successful",
                    EC = 0,
                    DT = token
                };
            }
            catch (Exception ex) // Bắt tất cả các ngoại lệ
            {
                _logger.LogError("Error during Google Login: {Message}", ex.Message);
                return new AuthResponeDto
                {
                    EM = "An error occurred during login",
                    EC = 1,
                    DT = null
                };
            }
        }

    }
}
