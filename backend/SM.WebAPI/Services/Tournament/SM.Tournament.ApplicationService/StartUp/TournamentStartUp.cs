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
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents.Statistic;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using SM.Tournament.ApplicationService.TourModule.Implements;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.ClubFundStatictic.Date;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.ClubFundStatictic.Rank;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Caculate.CaculateType;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic;
using SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic.Match;
using SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic.Player;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.ApplicationService.Minigame.Implements;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using SM.Tournament.ApplicationService.Minigame.Implements.Predict.PredictMatches;
using SM.Tournament.ApplicationService.Minigame.Implements.Vote;
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

            //builder.Services.AddScoped<EventFactorySerivce>();


            //Club Event 
            builder.Services.AddScoped<IEventStrategyUse, ClubEventStrategyUse>();

           
            builder.Services.AddKeyedScoped<IClubEventService,CelebrateService>("celebrate");
            builder.Services.AddKeyedScoped< IClubEventService,TeamMeetingService>("teamMeeting");
            builder.Services.AddKeyedScoped<IClubEventService, TrainingService>("training");


            builder.Services.AddKeyedScoped<IEventStatisticStrategy , ListEventOfPlayer>("listEventofplayer");
            builder.Services.AddKeyedScoped<IEventStatisticStrategy,ListPlayerOf1EventType>("listplayerofevent");



            //Club Fund
            builder.Services.AddScoped<IClubFundService, ClubFundService>();
            builder.Services.AddScoped<FundFactoryService>();
            builder.Services.AddScoped<ICaculateService, CaculateFundsService>();
            builder.Services.AddScoped<IFundStatisticService, FundStatisticService>();

            builder.Services.AddKeyedScoped<IFundStatisticStrategy, DayStatistic>("Day");
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, MonthStatistic>("Month");
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, WeekStatistic>("Week");
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, YearStatistic>("Year");
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, PlayerFundRank>("Rank");

            builder.Services.AddKeyedScoped<IFundCalculationStrategy , ContributeFundCaculation>("Contribute");
            builder.Services.AddKeyedScoped<IFundCalculationStrategy, DebtFundCalculation>("Debt");
            builder.Services.AddKeyedScoped<IFundCalculationStrategy, ExpenseFundCalculation>("Expense");
            builder.Services.AddKeyedScoped<IFundCalculationStrategy, ContributeFundCaculation>("ContributeTax");


            //matches
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, ClubMatchStatService>("clubMatch");
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, MatchesStatService>("matches");
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, PlayerMatchStatService>("playerMatch");
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, PlayerMatchStatService>("player");
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, TournamentMatchStatService>("tournamentMatch");
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, TournamentClubMatchStatService>("tournamentClub");
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, TournamentMatchService>("tournament");

            builder.Services.AddScoped<IMatchesService, MatchesService>();
            builder.Services.AddScoped<IMatchesStatisticService, MatchesStatisticService>();
            builder.Services.AddScoped<IMatchStatisticUse, MatchStrategyUse>();




            //minigame
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, MatchScore>("matchSCore");
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, NumberOfShot>("numberOfShot");
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, NumberOfPass>("numberOfPass");
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, NumberOfFouls>("numberOfFouls");
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, PlayerVote>("playerVote");

            builder.Services.AddScoped<IMatchStatBase, MatchStatBase>();
            builder.Services.AddScoped<IMinigameUse, MinigameUse>();
            builder.Services.AddScoped<IMinigameService, MinigameService>();
            builder.Services.AddScoped<IPredictService, PredictSerivce>();
            builder.Services.AddScoped<IVoteService, VoteService>();





            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ILineUpService, LineUpService>();

            builder.Services.AddScoped<IPlayerEventService , PlayerEventService>();
            builder.Services.AddScoped<IPlayerLineUpService, PlayerLineUpService>();

           


            builder.Services.AddScoped<ITournamentService, TournamentService>();








        }
    }
}
