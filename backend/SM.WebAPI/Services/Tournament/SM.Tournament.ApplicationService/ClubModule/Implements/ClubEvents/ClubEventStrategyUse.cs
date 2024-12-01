using Microsoft.Extensions.DependencyInjection;
using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents
{
    public class ClubEventStrategyUse : IEventStrategyUse
    {
        private readonly IClubEventService _celebrate;
        private readonly IClubEventService _training;
        private readonly IClubEventService _teamMeeting;

        private readonly IEventStatisticStrategy _listEventOfPlayer;
        private readonly IEventStatisticStrategy _listPlayerOf1EventType;

        public ClubEventStrategyUse(
            [FromKeyedServices(TourConst.Celebrate)] IClubEventService celebrate,
            [FromKeyedServices(TourConst.Training)] IClubEventService training,
            [FromKeyedServices(TourConst.TeamMeeting)] IClubEventService teamMeeting,
            [FromKeyedServices(TourConst.EventPlayer)] IEventStatisticStrategy listEventOfPlayer,
            [FromKeyedServices(TourConst.EventPlayerType)] IEventStatisticStrategy listPlayerOf1EventType
            )
        {
            _celebrate = celebrate;
            _training = training;
            _teamMeeting = teamMeeting;
            _listEventOfPlayer = listEventOfPlayer;
            _listPlayerOf1EventType = listPlayerOf1EventType;
        }

     
        public IClubEventService CreateEventsService(string serviceType)
        {
           
            return serviceType switch
            {
                TourConst.Celebrate => _celebrate, //_serviecProvider
                TourConst.Training => _training,
                TourConst.TeamMeeting => _teamMeeting,
                _ => throw new ArgumentException("Invalid service type", nameof(serviceType)),
            };
        }

        public IEventStatisticStrategy CreateEventStatisticStrategy(string type)
        {
            return type switch
            {
                TourConst.EventPlayer => _listEventOfPlayer,
                TourConst.EventPlayerType => _listPlayerOf1EventType,
                _ => throw new ArgumentException("Invalid event type", nameof(type)),
            };
        }

    }
}
