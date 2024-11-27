using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
    public class GoalPredict : BasePredict
    {
        public GoalPredict(
            ILogger<FoulsPredict> logger,
            TournamentDbContext dbContext,
            [FromKeyedServices("matches")] IMatchesStatisticStrategy matches,
            IReceiveAwardService receiveAwardService
             
        ) : base(logger, dbContext, matches, receiveAwardService)
        {


        }

        protected override int GetTeamStatistic(MatchStatisticsDto teamData, int half, string teamName, string statType)
        {
            if (statType.ToLower() == "goal")
            {
                return half switch
                {
                    1 => teamName == "TeamA" ? teamData.Half1.TeamA.Goal : teamData.Half1.TeamB.Goal,
                    2 => teamName == "TeamA" ? teamData.Half2.TeamA.Goal : teamData.Half2.TeamB.Goal,
                    _ => 0
                };
            }

            return 0; // Nếu statType không phải là "goal", trả về 0.
        }
    }

}
