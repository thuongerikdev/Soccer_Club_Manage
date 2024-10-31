using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SM.Player.Infrastructure;
using SM.Player.ApplicationService.Module.PlayerModule.Abtracts;
using SM.Player.ApplicationService.Module.PlayerModule.Implements;

namespace SM.Player.ApplicationService.Module.StartUp
{
    public static class PlayerStartUp
    {
        public static void ConfigurePlayer(this WebApplicationBuilder builder, string? assemblyName)
        {

            builder.Services.AddDbContext<PlayerDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Player
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
               
            );
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            
  


        }
    }
}
