using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
//using SM.Tournament.ApplicationService.Minigame.Implements.Predict.PredictMatches;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic.SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos.MinigameDto.Predict;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.Prediction
{
    public class MatchScoreStrategy : TournamentServiceBase, ICaculationResultStrategy
    {
        private readonly IMatchesStatisticStrategy _matches;
        public MatchScoreStrategy(ILogger<MatchScoreStrategy> logger, TournamentDbContext dbContext, [FromKeyedServices("matches")] IMatchesStatisticStrategy matches) : base(logger, dbContext)
        {
            _matches = matches;
        }
        public async Task<TournamentResponeDto> MinigameResult(string type, int minigameID)
        {
            //var predictions = new List<ReadPredictDto>(); // Khởi tạo danh sách để lưu trữ kết quả

            var minigame = await _dbContext.Minigames
     .FirstOrDefaultAsync(x => x.MinigameID == minigameID);

            int matchID = minigame.MatchesID;
            int handicap = minigame.Handicap;

            var matchStat = await _matches.getStatisTic(new ReadMatchesStatisticDto
            {
                MatchesID = matchID
            });

            var teamdata = matchStat.Data as MatchStatisticsDto;

            int teamAScoreH1 = teamdata.Half1.TeamA.Goal;
            int teamBScoreH1 = teamdata.Half1.TeamB.Goal;

            int teamAScoreH2 = teamdata.Half2.TeamA.Goal;
            int teamBScoreH2 = teamdata.Half2.TeamB.Goal;

            int totalScoreA = teamAScoreH1 + teamAScoreH2 + (handicap > 0 ? handicap : 0);
            int totalScoreB = teamBScoreH1 + teamBScoreH2 + (handicap < 0 ? -handicap : 0);


            switch (type)
            {
                case "h-1":
                    var result1 = _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                                    && x.TeamAscore == teamAScoreH1 + (handicap > 0 ? handicap : 0)
                                                                    && x.TeamBscore == teamBScoreH1 + (handicap < 0 ? -handicap : 0)
                                                                    && x.half == 1).ToList();
                    break;
                case "h-2":
                    var result2 = _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                                    && x.TeamAscore == teamAScoreH2 + (handicap > 0 ? handicap : 0)
                                                                    && x.TeamBscore == teamBScoreH2 + (handicap < 0 ? -handicap : 0)
                                                                    && x.half == 2).ToList();
                    break;
                case "full":
                    var result3 = _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                                    && x.TeamAscore == totalScoreA
                                                                    && x.TeamBscore == totalScoreB).ToList();
                    break;
            }

        }
    }
}
