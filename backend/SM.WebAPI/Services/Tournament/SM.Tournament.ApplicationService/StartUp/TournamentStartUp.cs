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
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubFund;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubEvents;
using SM.Tournament.ApplicationService.TournamentModule.PlayerModule.Abtracts;
using SM.Tournament.ApplicationService.TournamentModule.PlayerModule.Implements;
using SM.Tournament.ApplicationService.TournamentModule.LineUpModule.Abtracts;
using SM.Tournament.ApplicationService.TournamentModule.LineUpModule.Implements;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubFund.Factories;
namespace SM.Tournament.ApplicationService.Module.StartUp
{
    public static class TournamentStartUp
    {
        public static void ConfigureTournament(this WebApplicationBuilder builder, string? assemblyName)
        {

            builder.Services.AddDbContext<TournamentDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Tournament
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );
            builder.Services.AddScoped<IClubService, ClubService>();

            builder.Services.AddScoped<IClubFundService, ClubFundService>();
            builder.Services.AddScoped<FundFactoryService>();
            builder.Services.AddScoped<ICaculateService , CaculateFundsService>();

            builder.Services.AddScoped<EventFactorySerivce>();

            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ILineUpService, LineUpService>();





        }
    }
}
