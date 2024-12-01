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
//using SM.Tournament.ApplicationService.Minigame.Implements.Predict.PredictMatches;
using SM.Tournament.ApplicationService.Minigame.Implements.Vote;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.ApplicationService.OrderModule.Implements;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Predict;
using SM.Tournament.ApplicationService.Minigame.Implements.Predict.CreatPredict;
using SM.Tournament.ApplicationService.Minigame.Implements.Predict.Prediction;
using SM.Tournament.ApplicationService.Minigame.Services;
using SM.Tournament.ApplicationService.TourModule.Implements.TourMatches;
using SM.Tournament.Dtos.OrderDto.OrderModel.Momo;
using SM.Tournament.ApplicationService.NotificationModule.Abtracts;
using SM.Tournament.ApplicationService.NotificationModule.Implements;
using SM.Tournament.Domain.Notification;
using SM.Shared.ApplicationService.User;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Vote;
using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.PlayerStatistic;
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

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.AddScoped<IClubService, ClubService>();

            //builder.Services.AddScoped<EventFactorySerivce>();


            //Club Event 
            builder.Services.AddScoped<IEventStrategyUse, ClubEventStrategyUse>();

           
            builder.Services.AddKeyedScoped<IClubEventService,CelebrateService>(TourConst.Celebrate);
            builder.Services.AddKeyedScoped< IClubEventService,TeamMeetingService>(TourConst.TeamMeeting);
            builder.Services.AddKeyedScoped<IClubEventService, TrainingService>(TourConst.Training);


            builder.Services.AddKeyedScoped<IEventStatisticStrategy , ListEventOfPlayer>(TourConst.EventPlayer);
            builder.Services.AddKeyedScoped<IEventStatisticStrategy,ListPlayerOf1EventType>(TourConst.EventPlayerType);



            //Club Fund
            builder.Services.AddScoped<IFundStrategyUse, FundStrategyUse>();
            builder.Services.AddScoped<IClubFundService, ClubFundService>();
            builder.Services.AddScoped<ICaculateService, CaculateFundsService>();
            builder.Services.AddScoped<IFundStatisticService, FundStatisticService>();

            // Keyed services for IFundStatisticStrategy
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, DayStatistic>(TourConst.FundStatDay);
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, MonthStatistic>(TourConst.FundStatMonth);
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, WeekStatistic>(TourConst.FundStatWeek);
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, YearStatistic>(TourConst.FundStatYear);
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, PlayerFundRank>(TourConst.FundPlayerRank);

            builder.Services.AddKeyedScoped<IFundStatisticStrategy, PlayerFundTypeStatistic>(TourConst.FundPlayerType);
            builder.Services.AddKeyedScoped<IFundStatisticStrategy, PlayerSpecificTypeStatistic>(TourConst.FundPlayerSpecific);


            // Keyed services for IFundCalculationStrategy
            builder.Services.AddKeyedScoped<IFundCalculationStrategy, ContributeFundCaculation>(TourConst.FundContribute);
            builder.Services.AddKeyedScoped<IFundCalculationStrategy, DebtFundCalculation>(TourConst.FundDebt);
            builder.Services.AddKeyedScoped<IFundCalculationStrategy, ExpenseFundCalculation>(TourConst.FundExpense);
            builder.Services.AddKeyedScoped<IFundCalculationStrategy, ContributeWithTax>(TourConst.FundContributeTax);


            //matches
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, ClubMatchStatService>(TourConst.MatchClubStat);
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, MatchesStatService>(TourConst.MatchStat);
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, PlayerMatchStatService>(TourConst.MatchPlayerMatchStat);
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, PlayerStatService>(TourConst.MatchPlayerStat);
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, TournamentMatchStatService>(TourConst.MatchTournamentMatchStat);
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, TournamentClubMatchStatService>(TourConst.MatchTournamentClubStat);
            builder.Services.AddKeyedScoped<IMatchesStatisticStrategy, TournamentMatchService>(TourConst.MatchTournamentStat);

            builder.Services.AddScoped<IMatchesService, MatchesService>();
            builder.Services.AddScoped<IMatchesStatisticService, MatchesStatisticService>();
            builder.Services.AddScoped<IMatchStatisticUse, MatchStrategyUse>();
      
            builder.Services.AddKeyedScoped<IChooseTypePredict, halfChoose>(TourConst.HalfOrFullTime);
            builder.Services.AddKeyedScoped<IChooseTypePredict, PredictionType>(TourConst.PredictType);

            builder.Services.AddKeyedScoped<ICaculationResultStrategy, GoalPredict>(TourConst.PredictGoal);
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, PassPredict>(TourConst.PredictPass);
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, ShotPredict>(TourConst.PredictShot);
            builder.Services.AddKeyedScoped<ICaculationResultStrategy, FoulsPredict>(TourConst.PredictFoul);
            builder.Services.AddKeyedScoped<ICaculateVote, PlayerVote>(TourConst.PlayerVote);

            builder.Services.AddScoped<IReceiveAwardService, ReceiveAwardService>();

            // Thêm các chiến lược khác nếu cần.


            builder.Services.AddScoped<IMatchStatBase, MatchStatBase>();
            builder.Services.AddScoped<IMinigameUse, MinigameUse>();
            builder.Services.AddScoped<IMinigameService, MinigameService>();
            builder.Services.AddScoped<IPredictService, PredictSerivce>();
            builder.Services.AddScoped<IVoteService, VoteService>();
            builder.Services.AddScoped<IRewardService ,RewardService>();
            //builder.Services.AddScoped<IMinigameResultType, MinigameResultType>();





            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ILineUpService, LineUpService>();

            builder.Services.AddScoped<IPlayerEventService , PlayerEventService>();
            builder.Services.AddScoped<IPlayerLineUpService, PlayerLineUpService>();

           //Order
            builder.Services.AddScoped<IOrderService , OrderService>();

            //tournament
            builder.Services.AddScoped<ITournamentService, TournamentService>();
            builder.Services.AddScoped<ITournamentClubService, TournamentClubService>();
            builder.Services.AddScoped<ITourMatchStrategy, OneColumType>();



            builder.Services.AddScoped<IInvoiceService , InvoiceService>();
            builder.Services.AddScoped<IVnPayService, VnPayService>();
            builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
            builder.Services.AddScoped<IMomoService, MomoService>();

            //builder.Services.AddScoped<IUserInforSerivce, UserInforService>();






        }
    }
}
