using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.Domain.Club.ClubEvent;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubEvent.CelebrateEvent;
using SM.Tournament.Dtos.ClubDto.ClubEvent.ClubEvent;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubEvents
{
    public class CelebrateService : TournamentServiceBase , IClubEventService
    {
        public CelebrateService(ILogger <CelebrateService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async  Task <TournamentResponeDto> CreateEvent(CreateEventDto createCelebrateEventDto)
        {
            try {
                var celebrateEvent = new CelebrateEvent
                {
                    ClubID = createCelebrateEventDto.ClubID,
                    EventName = createCelebrateEventDto.EventName,
                    EventDescription = createCelebrateEventDto.EventDescription,
                    EventDate = createCelebrateEventDto.EventDate,
                    EventLocation = createCelebrateEventDto.EventLocation,
                    EventStatus = createCelebrateEventDto.EventStatus,
                    membersCount = createCelebrateEventDto.membersCount,
                    Decor = createCelebrateEventDto.Decor,
                    Menu = createCelebrateEventDto.Menu,
                    Music = createCelebrateEventDto.Music,
                    minigame = createCelebrateEventDto.minigame


                };
                _dbContext.CelebrateEvents.Add(celebrateEvent);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Celebrate Event Success",
                    Data = celebrateEvent.EventID
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
        public async Task<TournamentResponeDto> UpdateEvent(UpdateEventDto updateCelebrateEventDto)
        {
            try
            {
                var celebrateEvent = _dbContext.CelebrateEvents.FirstOrDefault(x => x.EventID == updateCelebrateEventDto.EventID);
                if (celebrateEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Event not found",
                        Data = null
                    };
                }
                celebrateEvent.EventName = updateCelebrateEventDto.EventName;
                celebrateEvent.EventDescription = updateCelebrateEventDto.EventDescription;
                celebrateEvent.EventDate = updateCelebrateEventDto.EventDate;
                celebrateEvent.EventLocation = updateCelebrateEventDto.EventLocation;
                celebrateEvent.EventStatus = updateCelebrateEventDto.EventStatus;
                celebrateEvent.membersCount = updateCelebrateEventDto.membersCount;
                celebrateEvent.Decor = updateCelebrateEventDto.Decor;
                celebrateEvent.Menu = updateCelebrateEventDto.Menu;
                celebrateEvent.Music = updateCelebrateEventDto.Music;
                celebrateEvent.minigame = updateCelebrateEventDto.minigame;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Celebrate Event Success",
                    Data = null
                };
            }
            catch (Exception ex) {
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
                var celebrateEvent = _dbContext.CelebrateEvents.FirstOrDefault(x => x.EventID == eventID);
                if (celebrateEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Event not found",
                        Data = null
                    };
                }
                _dbContext.CelebrateEvents.Remove(celebrateEvent);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Celebrate Event Success",
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
                var celebrateEvent = _dbContext.CelebrateEvents.FirstOrDefault(x => x.EventID == eventID);
                if (celebrateEvent == null)
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
                    ErrorMessage = "Get Celebrate Event Success",
                    Data = celebrateEvent
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
                var celebrateEvents = _dbContext.CelebrateEvents.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Celebrate Event Success",
                    Data = celebrateEvents
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
        public async Task<TournamentResponeDto> GetEventByClub(int clubID)
        {
            try
            {
                var celebrateEvents = _dbContext.CelebrateEvents.Where(x => x.ClubID == clubID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Celebrate Event By Club Success",
                    Data = celebrateEvents
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
