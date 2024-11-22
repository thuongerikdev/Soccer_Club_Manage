using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Auth.Infrastructure;
using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Auth.ApplicationService.UserModule.Abtracts;
using SM.Auth.ApplicationService.UserModule.Implements;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SM.Auth.ApplicationService.RoleModule.Abtracts;
using SM.Auth.ApplicationService.RoleModule.Implements;

namespace SM.Auth.ApplicationService.StartUp
{
    public static class AuthStartUp
    {
        public static void ConfigureAuth(this WebApplicationBuilder builder, string? assemblyName)
        {

            builder.Services.AddDbContext<AuthDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Auth
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );
            builder.Services.AddScoped<IAuthService , AuthService >();
            builder.Services.AddScoped<IAuthLoginService, AuthLoginService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
            builder.Services.AddScoped<IRoleService, RoleSerivce>();

           var secretKey = builder.Configuration["Jwt:SecretKey"];
            var key = Encoding.UTF8.GetBytes(secretKey);
            // Thay đổi secret key của bạn

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            })
            .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                    googleOptions.CallbackPath = "/signin-google"; // Đường dẫn này phải khớp với URI trong Google API Console
                });


        }
    }
}
