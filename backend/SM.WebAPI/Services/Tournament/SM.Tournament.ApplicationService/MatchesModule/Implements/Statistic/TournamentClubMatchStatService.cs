using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic
{
    public class TournamentClubMatchStatService : TournamentServiceBase, IMatchesStatisticStrategy
    {
        private readonly IMatchStatBase _matchStatBase;
        public TournamentClubMatchStatService(ILogger<TournamentClubMatchStatService> logger, TournamentDbContext dbContext, IMatchStatBase matchStatBase) : base(logger, dbContext)
        {
            _matchStatBase = matchStatBase;
        }
        public async Task<TournamentResponeDto> getStatisTic(ReadMatchesStatisticDto readMatchesStatisticDto)
        {
            var listmatch = await _dbContext.Matches
                .Where(x => x.TournamentID == readMatchesStatisticDto.TournamentID)
                .ToListAsync();

            // Initialize total statistics
            int totalFouls = 0;
            int totalYellowCard = 0;
            int totalRedCard = 0;
            int totalGoals = 0;
            int totalAssists = 0;
            int totalShots = 0;

            // Lặp qua từng trận đấu
            foreach (var item in listmatch)
            {
                var matchStats = await _dbContext.MatchesStatistics
                    .Where(x => x.ClubID == readMatchesStatisticDto.ClubID)
                    .ToListAsync();

                var convermatch = _matchStatBase.MapToCaculateStatisticDto(matchStats);
                var statResult = _matchStatBase.caculateStat(convermatch);

                // Assuming statResult.Data is dynamic
                var currentStats = (dynamic)statResult.Data;

                // Update total statistics
                totalFouls += currentStats.fouls;
                totalYellowCard += currentStats.yellowCard;
                totalRedCard += currentStats.redCard;
                totalGoals += currentStats.goals;
                totalAssists += currentStats.assists;
                totalShots += currentStats.shots;
            }

            // Create a summary of total statistics
            var totalStatDto = new
            {
                TotalFouls = totalFouls,
                TotalYellowCard = totalYellowCard,
                TotalRedCard = totalRedCard,
                TotalGoals = totalGoals,
                TotalAssists = totalAssists,
                TotalShots = totalShots
            };

            return new TournamentResponeDto
            {
                Data = totalStatDto, 
               
            };
        }
    }

    
}
