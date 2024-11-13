﻿using Microsoft.EntityFrameworkCore;
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
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Abtracts;
using SM.Tournament.ApplicationService.ClubModule.Implements;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund;
using SM.Tournament.ApplicationService.LineUpModule.Abtracts;
using SM.Tournament.ApplicationService.LineUpModule.Implements;
using SM.Tournament.ApplicationService.PlayerModule.Abtracts;
using SM.Tournament.ApplicationService.PlayerModule.Implements;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts;
using SM.Tournament.ApplicationService.MatchesModule.Implements;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Caculate;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.Caculate;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic;
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

            builder.Services.AddScoped<EventFactorySerivce>();

            builder.Services.AddScoped<IClubFundService, ClubFundService>();
            builder.Services.AddScoped<FundFactoryService>();
            builder.Services.AddScoped<ICaculateService , CaculateFundsService>();
            builder.Services.AddScoped<IFundStatisticService , FundStatisticService>();



            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ILineUpService, LineUpService>();

            builder.Services.AddScoped<IPlayerEventService , PlayerEventService>();
           

            builder.Services.AddScoped<IMatchesService, MatchesService>();
            builder.Services.AddScoped<IMatchesStatisticService, MatchesStatisticService>();

       





        }
    }
}
