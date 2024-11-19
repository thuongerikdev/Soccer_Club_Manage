using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic
{
    public class ClubMatchStatService : TournamentServiceBase , IMatchesStatisticStrategy

    {
        private readonly IMatchStatBase _matchStatBase;
        public ClubMatchStatService (ILogger<ClubMatchStatService> logger , TournamentDbContext dbContext , IMatchStatBase matchStatBase) : base (logger , dbContext)
        {
            _matchStatBase = matchStatBase;
        }
        public async Task<TournamentResponeDto> getStatisTic(ReadMatchesStatisticDto readMatchesStatisticDto)
        {
            var clubmatch = _dbContext.MatchesStatistics.Where(x => x.ClubID == readMatchesStatisticDto.ClubID).ToList();

            var converclub = _matchStatBase.MapToCaculateStatisticDto(clubmatch);
            var respone = _matchStatBase.caculateStat(converclub);
            return new TournamentResponeDto
            {
                ErrorCode = respone.ErrorCode,
                ErrorMessage = "success",
                Data = respone.Data
            };
        }
    }
}
