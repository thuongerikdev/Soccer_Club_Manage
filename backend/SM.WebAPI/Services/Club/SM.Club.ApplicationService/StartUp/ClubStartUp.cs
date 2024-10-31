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
using SM.Club.Infrastructure;
using SM.Club.ApplicationService.Module.ClubModule.Abtracts;
using SM.Club.ApplicationService.Module.ClubModule.Implements;


namespace SM.Club.ApplicationService.Module.StartUp
{
    public static class ClubStartUp
    {
        public static void ConfigureClub(this WebApplicationBuilder builder, string? assemblyName)
        {

            builder.Services.AddDbContext<ClubDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Club
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );
            builder.Services.AddScoped<IClubTeamService, ClubTeamService>();
  


        }
    }
}
