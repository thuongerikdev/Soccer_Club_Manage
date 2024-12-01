using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic.SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Predict;
using SM.Constant.Tournament;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.Prediction
{
    public abstract class BasePredict : TournamentServiceBase, ICaculationResultStrategy
    {
        private readonly IMatchesStatisticStrategy _matches;
        private readonly IReceiveAwardService _receiveAwardService;
      

        public BasePredict(
            ILogger <BasePredict> logger,
            TournamentDbContext dbContext,
            [FromKeyedServices(TourConst.MatchStat)] IMatchesStatisticStrategy matches,
            IReceiveAwardService receiveAwardService
          
        ) : base(logger, dbContext)
        {
            _matches = matches;
            _receiveAwardService = receiveAwardService;
          
        }

        protected abstract int GetTeamStatistic(MatchStatisticsDto teamData, int half, string teamName, string statType);
        
          

        public async Task<TournamentResponeDto> MinigameResult(int half, int minigameID, string type , string topic)
        {
            var minigame = await _dbContext.Minigames
                .FirstOrDefaultAsync(x => x.MinigameID == minigameID);

            if (minigame == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Minigame not found",
                    Data = null
                };
            }
            var minigameType = minigame.MinigameType;

            int matchID = minigame.MatchesID;
            int handicap = minigame.Handicap;

            var matchStat = await _matches.getStatisTic(new ReadMatchesStatisticDto
            {
                MatchesID = matchID
            });

            var teamData = matchStat.Data as MatchStatisticsDto;
            if (teamData == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 2,
                    ErrorMessage = "Match statistics not available",
                    Data = null
                };
            }

            // Fetch statistics for each type (Goal, Pass, Shot, Fouls)
            int teamAStatH1 = GetTeamStatistic(teamData, 1, TourConst.TeamA, type);
            int teamBStatH1 = GetTeamStatistic(teamData, 1, TourConst.TeamB, type);



            int teamAStatH2 = GetTeamStatistic(teamData, 2, TourConst.TeamA, type);
            int teamBStatH2 = GetTeamStatistic(teamData, 2, TourConst.TeamB, type);

            int totalStatAH1 = teamAStatH1 + (handicap > 0 ? handicap : 0);
            int totalStatBH1 = teamBStatH1 + (handicap < 0 ? -handicap : 0);
            int totalStatAH2 = teamAStatH2 + (handicap > 0 ? handicap : 0);
            int totalStatBH2 = teamBStatH2 + (handicap < 0 ? -handicap : 0);

            int totalStatA = teamAStatH1 + teamAStatH2;
            int totalStatB = teamBStatH1 + teamBStatH2;

            int totalH1 = teamAStatH1 + teamBStatH1;
            int totalH2 = teamAStatH2 + teamBStatH2;
            int total = totalStatA + totalStatB;

            bool? oddEvenTotal = (total % 2 == 0) ? false : true;
            bool? oddEvenH1 = (totalH1 % 2 == 0) ? false : true;
            bool? oddEvenH2 = (totalH2 % 2 == 0) ? false : true;

            var result = new List<Predictions>();

            switch (minigameType.ToLower())
            {
                case "matchscore":
                    result = GetMatchResult(half, minigameID, totalStatAH1, totalStatBH1, totalStatAH2, totalStatBH2, totalStatA, totalStatB);
                    break;

                case "oddeven":
                    result = GetOddEvenResult(half, minigameID, oddEvenH1, oddEvenH2, oddEvenTotal);
                    break;

                case "predicttotal":
                    result = GetPredictTotalResult(half, minigameID, totalH1, totalH2, total);
                    break;

                default:
                    return new TournamentResponeDto
                    {
                        ErrorCode = 3,
                        ErrorMessage = "Invalid type specified",
                        Data = null
                    };
            }

            var receiveRank = await _receiveAwardService.AwardRewardsAsync(minigameID, result);
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Success",
                Data = receiveRank

            };
        }
     

    private List<Predictions> GetMatchResult(int half, int minigameID, int teamAStatH1, int teamBStatH1, int teamAStatH2, int teamBStatH2, int totalStatA, int totalStatB)
        {
            return half switch
            {
                1 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.TeamAscore == teamAStatH1
                                                        && x.TeamBscore == teamBStatH1
                                                        && x.half == 1).ToList(),
                2 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.TeamAscore == teamAStatH2
                                                        && x.TeamBscore == teamBStatH2
                                                        && x.half == 2).ToList(),
                0 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.TeamAscore == totalStatA
                                                        && x.TeamBscore == totalStatB).ToList(),
                _ => new List<Predictions>()
            };
        }

        private List<Predictions> GetOddEvenResult(int half, int minigameID, bool? oddEvenH1, bool? oddEvenH2, bool? oddEvenTotal)
        {
            return half switch
            {
                1 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.OddEven == oddEvenH1
                                                        && x.half == 1).ToList(),
                2 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.OddEven == oddEvenH2
                                                        && x.half == 2).ToList(),
                0 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.OddEven == oddEvenTotal).ToList(),
                _ => new List<Predictions>()
            };
        }

        private List<Predictions> GetPredictTotalResult(int half, int minigameID, int totalH1, int totalH2, int total)
        {
            return half switch
            {
                1 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.PredictTotal == totalH1
                                                        && x.half == 1).ToList(),
                2 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.PredictTotal == totalH2
                                                        && x.half == 2).ToList(),
                0 => _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.PredictTotal == total).ToList(),
                _ => new List<Predictions>()
            };
        }
    }



}
