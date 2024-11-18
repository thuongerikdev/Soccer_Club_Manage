using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Domain.Match;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic.Match
{
    public class MatchesStatService : TournamentServiceBase, IMatchesStatisticStrategy
    {
        private readonly IMatchStatBase _matchStatBase;

        public MatchesStatService(ILogger<MatchesStatService> logger, TournamentDbContext dbContext, IMatchStatBase matchStatBase)
            : base(logger, dbContext)
        {
            _matchStatBase = matchStatBase;
        }

        public async Task<TournamentResponeDto> getStatisTic(ReadMatchesStatisticDto readMatchesStatisticDto)
        {
            var teamAStats = _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID == readMatchesStatisticDto.ClubID)
                .ToList();

            var teamBStats = _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID != readMatchesStatisticDto.ClubID)
                .ToList();


            // Map MatchesStatistic to CaculateStatisticDto
            var teamAStatsDto = _matchStatBase.MapToCaculateStatisticDto(teamAStats);
            var teamBStatsDto = _matchStatBase.MapToCaculateStatisticDto(teamBStats);

            // Calculate statistics using the base class
            var resultTeamA = _matchStatBase.caculateStat(teamAStatsDto);
            var resultTeamB = _matchStatBase.caculateStat(teamBStatsDto);

            var result = new
            {
                teamA = new
                {
                    additionalStats = resultTeamA.Data // Include additional calculated stats
                },
                teamB = new
                {
                    additionalStats = resultTeamB.Data // Include additional calculated stats
                }
            };

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Match Statistics Success",
                Data = result
            };
        }

        // Mapping method to convert MatchesStatistic to CaculateStatisticDto
      
    }
}