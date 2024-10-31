using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Constant.Database;
using SM.LineUp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.ApplicationService.StartUp
{
    public static class LineUpStartUp
    {
        public static void ConfigureAuth(this WebApplicationBuilder builder, string? assemblyName)
        {

            builder.Services.AddDbContext<LineUpDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.LineUp
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );


        }
    }
}
