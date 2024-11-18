using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents
{
    public interface IEventStrategyUse
    {
        //public Task<TournamentResponeDto> EventStatistic(ReadPlayerEventDto readPlayerEventDto);
        public IEventStatisticStrategy CreateEventStatisticStrategy(string type);
        public IClubEventService CreateEventsService(string serviceType);
    }
}
