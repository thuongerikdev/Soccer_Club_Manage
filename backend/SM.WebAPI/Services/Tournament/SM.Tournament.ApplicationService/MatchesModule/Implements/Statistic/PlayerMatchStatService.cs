using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic.Player
{
    public class PlayerMatchStatService : TournamentServiceBase, IMatchesStatisticStrategy
    {
        private readonly IMatchStatBase _matchStatBase;
        public PlayerMatchStatService(ILogger<PlayerMatchStatService> logger, TournamentDbContext dbContext, IMatchStatBase matchStatBase)
            : base(logger, dbContext)
        {
            _matchStatBase = matchStatBase;
        }

        public async Task<TournamentResponeDto> getStatisTic(ReadMatchesStatisticDto readMatchesStatisticDto)
        {
            var playerStats = _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID
                             && x.ClubID == readMatchesStatisticDto.ClubID
                             && x.PlayerID == readMatchesStatisticDto.PlayerID)
                .ToList();


            var playerStatsDto = _matchStatBase.MapToCaculateStatisticDto(playerStats);
            var resultPlayer = _matchStatBase.caculateStat(playerStatsDto);

       

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Player Statistics Success",
                Data = resultPlayer
            };
        }
    }
}