//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using SM.Tournament.ApplicationService.Common;
//using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
//using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
//using SM.Tournament.Dtos;
//using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
//using SM.Tournament.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.PredictMatches
//{
//    public class MatchScore : TournamentServiceBase, ICaculationResultStrategy
//    {
//        private readonly IMatchesStatisticStrategy _matches;
//        public MatchScore(ILogger<MatchScore> logger, TournamentDbContext dbContext, [FromKeyedServices("matches")] IMatchesStatisticStrategy matches) : base(logger, dbContext)
//        {
//            _matches = matches;
//        }
//        public async Task<TournamentResponeDto> MinigameResult(int minigameID)
//        {
           

//            var predicts = _dbContext.Predictions.FirstOrDefault(x => x.MinigameID == minigameID);
//            if (predicts == null)
//            {
//                return new TournamentResponeDto
//                {
//                    ErrorCode = 1 ,
//                    ErrorMessage = "No data found",
//                    Data = null
//                };
//            }
//            var matchID = predicts.MatchID;
//            var match = new ReadMatchesStatisticDto
//            {
//                ClubID = 0,
//                MatchesID = matchID,
//                PlayerID = 0,
//                TournamentID = 0

//            };
//            var matchStat = await _matches.getStatisTic(match);
//            var teamAdata = matchStat.Data as ScoreStatisticDto; // Assuming the data returned here has TeamA and TeamB

//            var teamAScore = teamAdata.TeamA.Goal;
//            var teamBScore = teamAdata.TeamB.Goal;


//            var result = _dbContext.Predictions.Where(x => x.MinigameID == minigameID
//                                                        && x.Prediction == $"{teamAScore}-{teamBScore}").ToList();

//            //foreach (var item in result) { 

//            //}

//            return new TournamentResponeDto
//            {
//                ErrorCode = 0,
//                ErrorMessage = "Get Winner Success",
//                Data = result
//            };

//        }
//    }
//}
