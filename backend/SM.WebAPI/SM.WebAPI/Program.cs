
using SM.Auth.ApplicationService.StartUp;
using SM.Tournament.ApplicationService.Module.StartUp;
using SM.Club.ApplicationService.Module.StartUp;
using SM.Player.ApplicationService.Module.StartUp;
using SM.Match.ApplicationService.Module.StartUp;
using SM.LineUp.ApplicationService.Module.StartUp;

namespace SM.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.ConfigureAuth(typeof(Program).Namespace);
            builder.ConfigureClub(typeof(Program).Namespace);
            builder.ConfigurePlayer(typeof(Program).Namespace);
            //builder.ConfigureTournament(typeof(Program).Namespace);
            builder.ConfigureMatches(typeof(Program).Namespace);
            builder.ConfigureLineUp(typeof(Program).Namespace);




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();

            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
