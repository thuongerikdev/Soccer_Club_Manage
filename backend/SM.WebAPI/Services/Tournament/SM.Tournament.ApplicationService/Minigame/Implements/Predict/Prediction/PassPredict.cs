using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Predict;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic.SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.Prediction
{
    public class PassPredict : BasePredict
    {
        public PassPredict(
             ILogger<FoulsPredict> logger,
            TournamentDbContext dbContext,
            [FromKeyedServices(TourConst.MatchStat)] IMatchesStatisticStrategy matches,
            IReceiveAwardService receiveAwardService
         
        ) : base(logger, dbContext, matches, receiveAwardService)
        {


        }
        protected override int GetTeamStatistic(MatchStatisticsDto teamData, int half, string teamName, string statType)
        {
            if (statType.ToLower() == TourConst.PredictPass)
            {
                return half switch
                {
                    1 => teamName == TourConst.TeamA ? teamData.Half1.TeamA.Pass : teamData.Half1.TeamB.Pass,
                    2 => teamName == TourConst.TeamB ? teamData.Half2.TeamA.Pass : teamData.Half2.TeamB.Pass,
                    _ => 0
                };
            }

            return 0; // Nếu statType không phải là "pass", trả về 0.
        }
    }

}
