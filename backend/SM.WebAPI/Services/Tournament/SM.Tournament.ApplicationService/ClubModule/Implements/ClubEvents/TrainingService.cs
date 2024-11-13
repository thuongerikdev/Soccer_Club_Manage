using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Domain.Club.ClubEvent;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubEvent.ClubEvent;
using SM.Tournament.Dtos.ClubDto.ClubEvent.TrainingEvent;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents
{
    public class TrainingService : TournamentServiceBase, IClubEventService
    {
        public TrainingService(ILogger<TrainingService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreateEvent(CreateEventDto createTrainingEventDto)
        {
            try
            {
                var trainingEvent = new TrainingEvent
                {
                    ClubID = createTrainingEventDto.ClubID,
                    EventName = createTrainingEventDto.EventName,
                    EventDescription = createTrainingEventDto.EventDescription,
                    EventDate = createTrainingEventDto.EventDate,
                    EventLocation = createTrainingEventDto.EventLocation,
                    EventStatus = createTrainingEventDto.EventStatus,
                    membersCount = createTrainingEventDto.membersCount,
                    TrainingAim = createTrainingEventDto.TrainingAim,
                    LessonTraining = createTrainingEventDto.LessonTraining,
                    Coach = createTrainingEventDto.Coach,
                    ProcessResult = createTrainingEventDto.ProcessResult,
                };
                _dbContext.TrainingEvents.Add(trainingEvent);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Training Event Success",
                    Data = trainingEvent.EventID
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }

        }
        public async Task<TournamentResponeDto> UpdateEvent(UpdateEventDto updateTrainingEventDto)
        {
            try
            {
                var trainingEvent = _dbContext.TrainingEvents.FirstOrDefault(x => x.EventID == updateTrainingEventDto.EventID);
                if (trainingEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Event not found",
                        Data = null
                    };
                }
                trainingEvent.EventName = updateTrainingEventDto.EventName;
                trainingEvent.EventDescription = updateTrainingEventDto.EventDescription;
                trainingEvent.EventDate = updateTrainingEventDto.EventDate;
                trainingEvent.EventLocation = updateTrainingEventDto.EventLocation;
                trainingEvent.EventStatus = updateTrainingEventDto.EventStatus;
                trainingEvent.membersCount = updateTrainingEventDto.membersCount;
                trainingEvent.TrainingAim = updateTrainingEventDto.TrainingAim;
                trainingEvent.LessonTraining = updateTrainingEventDto.LessonTraining;
                trainingEvent.Coach = updateTrainingEventDto.Coach;
                trainingEvent.ProcessResult = updateTrainingEventDto.ProcessResult;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Training Event Success",
                    Data = trainingEvent.EventID
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> RemoveEvent(int eventID)
        {
            try
            {
                var trainingEvent = _dbContext.TrainingEvents.FirstOrDefault(x => x.EventID == eventID);
                if (trainingEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Event not found",
                        Data = null
                    };
                }
                _dbContext.TrainingEvents.Remove(trainingEvent);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Training Event Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetEventById(int eventID)
        {
            try
            {
                var trainingEvent = _dbContext.TrainingEvents.FirstOrDefault(x => x.EventID == eventID);
                if (trainingEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Event not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Training Event Success",
                    Data = trainingEvent
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetAllEvents()
        {
            try
            {
                var trainingEvents = _dbContext.TrainingEvents.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Training Event Success",
                    Data = trainingEvents
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetEventByClubId(int clubID)
        {
            try
            {
                var trainingEvents = _dbContext.TrainingEvents.Where(x => x.ClubID == clubID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Training Event By ClubID Success",
                    Data = trainingEvents
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetEventByDate(DateTime eventDate)
        {
            try
            {
                var trainingEvents = _dbContext.TrainingEvents.Where(x => x.EventDate == eventDate).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Training Event By Date Success",
                    Data = trainingEvents
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }


    }
}
