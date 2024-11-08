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
using SM.Match.Infrastructure;
using SM.Match.ApplicationService.Module.MatchesModule.Abtracts;
using SM.Match.ApplicationService.Module.MatchesModule.Implements;
using SM.Tournament.ApplicationService.Module.MatchesModule.Implements;


namespace SM.Match.ApplicationService.Module.StartUp
{
    public static class MatchStartUp
    {
        public static void ConfigureMatches(this WebApplicationBuilder builder, string? assemblyName)
        {

            builder.Services.AddDbContext<MatchDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Match
                            );
                        }
                    );
                },
                
            ServiceLifetime.Scoped
            );
            builder.Services.AddScoped<IMatchesService, MatchesService>();
            builder.Services.AddScoped<IMatchesStatisticService, MatchesStatisticService>();




        }
    }
}
