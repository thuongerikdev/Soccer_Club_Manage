using Microsoft.Extensions.DependencyInjection;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents
{
    public class ClubEventStrategyUse : IEventStrategyUse
    {
        private IEventStatisticStrategy _eventStatisticStrategy;
        private readonly IClubEventService _celebrate;
        private readonly IClubEventService _training;
        private readonly IClubEventService _teamMeeting;

        private readonly IEventStatisticStrategy _listEventOfPlayer;
        private readonly IEventStatisticStrategy _listPlayerOf1EventType;

        public ClubEventStrategyUse(
            [FromKeyedServices("celebrate")] IClubEventService celebrate,
            [FromKeyedServices("training")] IClubEventService training,
            [FromKeyedServices("teamMeeting")] IClubEventService teamMeeting,
            [FromKeyedServices("listEventofplayer")] IEventStatisticStrategy listEventOfPlayer,
            [FromKeyedServices("listplayerofevent")] IEventStatisticStrategy listPlayerOf1EventType
            )
        {
            _celebrate = celebrate;
            _training = training;
            _teamMeeting = teamMeeting;
            _listEventOfPlayer = listEventOfPlayer;
            _listPlayerOf1EventType = listPlayerOf1EventType;
        }

        //public async Task<TournamentResponeDto> EventStatistic(ReadPlayerEventDto readPlayerEventDto)
        //{
        //    return await _eventStatisticStrategy.EventStatistic(readPlayerEventDto);
        //}

        public IClubEventService CreateEventsService(string serviceType)
        {
            return serviceType switch
            {
                "Celebrate" => _celebrate, //_serviecProvider
                "Training" => _training,
                "TeamMeeting" => _teamMeeting,
                _ => throw new ArgumentException("Invalid service type", nameof(serviceType)),
            };
        }

        public IEventStatisticStrategy CreateEventStatisticStrategy(string type)
        {
            return type switch
            {
                "ListEventOfPlayer" => _listEventOfPlayer,
                "ListPlayerOf1EventType" => _listPlayerOf1EventType,
                _ => throw new ArgumentException("Invalid event type", nameof(type)),
            };
        }

    }
}
