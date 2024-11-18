using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic
{
    public interface IMatchesStatisticStrategy
    {
        public Task <TournamentResponeDto> getStatisTic (ReadMatchesStatisticDto readMatchesStatisticDto);
    }
}
