using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents.Statistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubEvent.ClubEvent;
using SM.Tournament.Infrastructure;



namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents
{
    public class EventFactorySerivce
    {
        private readonly ILogger<CelebrateService> _celebrateLogger;
        private readonly ILogger<TeamMeetingService> _teamMeetingLogger;
        private readonly ILogger<TrainingService> _trainingService;

        private readonly ILogger<ListEventOfPlayer> _listPlayerMember;
        private readonly ILogger<ListPlayerOf1EventType> _listPlayerOf1Type;

        private readonly TournamentDbContext _dbContext;

        public EventFactorySerivce(
            ILogger<CelebrateService> celebrateLogger,
            ILogger<TeamMeetingService> teamMeetingLogger,
            ILogger<TrainingService> trainingService,

            ILogger<ListEventOfPlayer> listPlayerMember,
            ILogger<ListPlayerOf1EventType> listPlayerOf1Type,

            TournamentDbContext dbContext)
        {
            _celebrateLogger = celebrateLogger;
            _teamMeetingLogger = teamMeetingLogger;
            _trainingService = trainingService;

            _listPlayerMember = listPlayerMember;
            _listPlayerOf1Type = listPlayerOf1Type;

            _dbContext = dbContext;
        }

        public object CreateService(string serviceType)
        {
            if (serviceType == "Celebrate")
            {
                return new CelebrateService(_celebrateLogger, _dbContext);

            }
            else if (serviceType == "TeamMeeting")
            {
                return new TeamMeetingService(_teamMeetingLogger, _dbContext);
            }
            else if (serviceType == "Training")
            {
                return new TrainingService(_trainingService, _dbContext);
            }
            else
            {
                throw new ArgumentException("Invalid service type");
            }
        }
        public IEventStatisticStrategy eventStatisticStrategy  (string Type)
        {
            return Type switch
            {
                "ListEventofPlayer" => new ListEventOfPlayer(_listPlayerMember, _dbContext),
                "ListPlayerOf1EventType" => new ListPlayerOf1EventType(_listPlayerOf1Type, _dbContext),


                _ => throw new ArgumentException("Invalid event type", nameof(Type))
            };


        }
    }
}