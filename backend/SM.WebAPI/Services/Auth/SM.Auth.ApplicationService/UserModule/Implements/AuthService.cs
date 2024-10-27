using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SM.Auth.ApplicationService.Common;
using SM.Auth.ApplicationService.UserModule.Abtracts;
using SM.Auth.Domain;
using SM.Auth.Dtos.CRUDModule;
using SM.Auth.Dtos.LoginModule;
using SM.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.ApplicationService.UserModule.Implements
{
    public  class AuthService: AuthServiceBase , IAuthService
    {
       public AuthService( ILogger<AuthService> logger , AuthDbContext dbContext): base(logger , dbContext)
        {

        }



        public async Task<AuthResponeDto> AuthRegisterAsync(AuthRegisterDto authRegisterDto)
        {
            try
            {
                var user = new AuthUser
                {
                    username = authRegisterDto.username,
                    password = authRegisterDto.password,
                    email = authRegisterDto.email,
                    age = 0,
                    address = "",
                    gender =  "",
                    phone = 0,
                    name = ""
                };

                _dbContext.AuthUsers.Add(user);
                await _dbContext.SaveChangesAsync();

                return new AuthResponeDto
                {
                    EM = "Register success",
                    EC = 0,
                    DT = null // Hoặc mảng chuỗi nếu cần
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during registration for user: {Username}", authRegisterDto.username);
                return new AuthResponeDto
                {
                    EM = ex.Message,
                    EC = 1,
                    DT = null
                };
            }
        }
        public async Task<AuthResponeDto> AuthRemove(int userId)
        {
            try
            {
                var user = await _dbContext.AuthUsers.FirstOrDefaultAsync(x => x.userId == userId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                _dbContext.AuthUsers.Remove(user);
                await _dbContext.SaveChangesAsync();

                return new AuthResponeDto
                {
                    EM = "Remove success",
                    EC = 0,
                    DT = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing user with ID: {UserId}", userId);
                return new AuthResponeDto
                {
                    EM = ex.Message,
                    EC = 1,
                    DT = null
                };
            }
        }

        public async Task<AuthResponeDto> AuthUpdate(int userId, AuthUpdateDto authUpdateDto)
        {
            try
            {
                var existingUser = await _dbContext.AuthUsers.FirstOrDefaultAsync(x => x.userId == userId);
                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                existingUser.username = authUpdateDto.username;
                existingUser.password = authUpdateDto.password;
                existingUser.email = authUpdateDto.email;
                existingUser.age = authUpdateDto.age;
                existingUser.address = authUpdateDto.address;
                existingUser.gender = authUpdateDto.gender;
                existingUser.phone = authUpdateDto.phone;

                await _dbContext.SaveChangesAsync();

                return new AuthResponeDto
                {
                    EM = "Update success",
                    EC = 0,
                    DT = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user with ID: {UserId}", userId);
                return new AuthResponeDto
                {
                    EM = ex.Message,
                    EC = 1,
                    DT = null
                };
            }
        }
        public async ValueTask <IEnumerable<AuthGetAllUserDto>> AuthGetAll()
        {
            try
            {
                // Lấy danh sách người dùng từ cơ sở dữ liệu
                var users = await _dbContext.AuthUsers.ToListAsync();

                // Ánh xạ danh sách người dùng sang DTO
                var userDtos = users.Select(x => new AuthGetAllUserDto
                {
                    userId = x.userId,
                    username = x.username,
                    password = x.password,
                    email = x.email,
                    age = x.age,
                    address = x.address,
                    gender = x.gender,
                    phone = x.phone,
                }).ToList();

                return userDtos;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }
    }
}
