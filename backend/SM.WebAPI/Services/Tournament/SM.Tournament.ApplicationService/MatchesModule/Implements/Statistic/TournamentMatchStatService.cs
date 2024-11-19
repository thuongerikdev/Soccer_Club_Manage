

using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Tournament.ApplicationService.Common;
using Microsoft.Extensions.Logging;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic
{
    public class TournamentMatchStatService : TournamentServiceBase, IMatchesStatisticStrategy
    {
        private readonly IMatchStatBase _matchStatBase;
        public TournamentMatchStatService(ILogger<TournamentMatchStatService> logger, TournamentDbContext dbContext, IMatchStatBase matchStatBase) : base(logger, dbContext)
        {
            _matchStatBase = matchStatBase;
        }
        public async Task<TournamentResponeDto> getStatisTic(ReadMatchesStatisticDto readMatchesStatisticDto)
        {
            var match = _dbContext.Matches.FirstOrDefault(x => x.MatchesID == readMatchesStatisticDto.MatchesID
                                                      && x.TournamentID == readMatchesStatisticDto.TournamentID);
            var tourmatch = _dbContext.MatchesStatistics.Where(x => x.MatchesID == match.MatchesID).ToList();

            var convermatch = _matchStatBase.MapToCaculateStatisticDto(tourmatch);
            var respone = _matchStatBase.caculateStat(convermatch);
            return new TournamentResponeDto
            {
                ErrorCode = respone.ErrorCode,
                ErrorMessage = "success",
                Data = respone.Data
            };
        }
    }
}
