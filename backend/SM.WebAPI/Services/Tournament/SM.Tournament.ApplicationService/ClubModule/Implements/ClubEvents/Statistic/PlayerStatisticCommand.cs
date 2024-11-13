using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents.Statistic
{
    public class PlayerStatisticCommand
    {
        private IEventStatisticStrategy _eventStatisticStrategy;
        private ReadPlayerEventDto _readPlayerEventDto;
        public PlayerStatisticCommand(IEventStatisticStrategy eventStatisticStrategy , ReadPlayerEventDto readPlayerEventDto)
        {
            _eventStatisticStrategy = eventStatisticStrategy;
            _readPlayerEventDto = readPlayerEventDto;
        }
        public async Task<TournamentResponeDto> EventStatistic()
        {
            return await _eventStatisticStrategy.EventStatistic(_readPlayerEventDto);
        }

    }
}
