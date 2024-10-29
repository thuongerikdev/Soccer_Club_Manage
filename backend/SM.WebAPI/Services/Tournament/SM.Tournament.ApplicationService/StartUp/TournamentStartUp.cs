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
using SM.Tournament.Infrastructure;
using SM.Tournament.ApplicationService.Module.ClubModule.Abtracts;
using SM.Tournament.ApplicationService.Module.ClubModule.Implements;
namespace SM.Tournament.ApplicationService.Module.StartUp
{
    public static class TournamentStartUp
    {
        public static void ConfigureTournament(this WebApplicationBuilder builder, string? assemblyName)
        {

            builder.Services.AddDbContext<TournamentDBContext>(
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
            builder.Services.AddScoped<IClubTeamService, ClubTeamService>();
  


        }
    }
}
