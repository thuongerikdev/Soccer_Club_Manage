using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Predict.PredictMatches
{
    public class NumberOfShot : TournamentServiceBase, ICaculationResultStrategy
    {
        private readonly IMatchesStatisticStrategy _matches;

        public NumberOfShot(ILogger<NumberOfShot> logger, TournamentDbContext dbContext, [FromKeyedServices("matches")] IMatchesStatisticStrategy matches)
            : base(logger, dbContext)
        {
            _matches = matches;
        }

        public async Task<TournamentResponeDto> MinigameResult(int minigameID)
        {
            var predicts = _dbContext.Predictions.FirstOrDefault(x => x.MinigameID == minigameID);
            if (predicts == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "No data found",
                    Data = null
                };
            }
            var matchID = predicts.MatchID;
            var matches = await _matches.getStatisTic(new ReadMatchesStatisticDto
            {
                ClubID = 0,
                MatchesID = matchID,
                PlayerID = 0,
                TournamentID = 0
            });
            var shotData = matches.Data as CaculateStatisticDto;
            if (shotData != null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "No data found",
                    Data = null
                };
            }
            var totalShot = shotData.Shot;
            var result = _dbContext.Predictions.Where(x => x.MinigameID == minigameID
                                                        && x.Prediction == totalShot.ToString()).ToList();

            // Thêm logic xử lý cho MinigameResult
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Minigame result processed successfully.",
                Data = result // Cập nhật dữ liệu thực tế ở đây
            };
        }
    }
}