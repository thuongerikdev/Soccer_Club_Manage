using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic
{
    public interface IEventStatisticStrategy
    {
        public Task<TournamentResponeDto> EventStatistic(ReadPlayerEventDto readPlayerEventDto);
    }
}
