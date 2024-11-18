using SM.Tournament.Domain.Match;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic
{
    public interface IMatchStatBase
    {
        public TournamentResponeDto caculateStat(List<CaculateStatisticDto> stat);
        public List<CaculateStatisticDto> MapToCaculateStatisticDto(List<MatchesStatistic> stats);
    }
}
