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
            var matchstat = _dbContext.Matches.FirstOrDefault(Matches => Matches.MatchesID == readMatchesStatisticDto.MatchesID);

            var teamAStats = _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID == matchstat.TeamA)
                .ToList();

            var teamBStats = _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID == matchstat.TeamB)
                .ToList();


            // Map MatchesStatistic to CaculateStatisticDto
            var teamAStatsDto = _matchStatBase.MapToCaculateStatisticDto(teamAStats);
            var teamBStatsDto = _matchStatBase.MapToCaculateStatisticDto(teamBStats);

            // Calculate statistics using the base class
            var resultTeamA = _matchStatBase.caculateStat(teamAStatsDto);
            var resultTeamB = _matchStatBase.caculateStat(teamBStatsDto);

            // Chuyển đổi Data thành CaculateStatisticDto
            var teamAData = resultTeamA.Data as CaculateStatisticDto;
            var teamBData = resultTeamB.Data as CaculateStatisticDto;

            var result = new ScoreStatisticDto
            {
                TeamA = new CaculateStatisticDto
                {
                   Goal = teamAData.Goal,
                   Pass = teamAData.Pass,
                   Shot = teamAData.Shot,
                    YellowCard = teamAData.YellowCard,
                    RedCard = teamAData.RedCard,
                    Fouls = teamAData.Fouls,
                   
                },
                TeamB = new CaculateStatisticDto
                {
                    Goal = teamBData.Goal,
                    Pass = teamBData.Pass,
                    Shot = teamBData.Shot,
                    YellowCard = teamBData.YellowCard,
                    RedCard = teamBData.RedCard,
                    Fouls = teamBData.Fouls,
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