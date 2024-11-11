using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubEvents;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubEvent.ClubEvent;
using SM.Tournament.Infrastructure;



namespace SM.Tournament.ApplicationService.Common
{
    public class EventFactorySerivce 
    {
        private readonly ILogger<CelebrateService> _celebrateLogger;
        private readonly ILogger<TeamMeetingService> _teamMeetingLogger;
        private readonly ILogger<TrainingService> _trainingService;
        private readonly TournamentDbContext _dbContext;

        public EventFactorySerivce(
            ILogger<CelebrateService> celebrateLogger,
            ILogger<TeamMeetingService> teamMeetingLogger,
            ILogger<TrainingService> trainingService,
            TournamentDbContext dbContext)
        {
            _celebrateLogger = celebrateLogger;
            _teamMeetingLogger = teamMeetingLogger;
            _trainingService = trainingService;
            _dbContext = dbContext;
        }

        public object CreateService(string serviceType)
        {
            if (serviceType == "Celebrate")
            {
                return   new CelebrateService(_celebrateLogger, _dbContext);
         
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
    }
}