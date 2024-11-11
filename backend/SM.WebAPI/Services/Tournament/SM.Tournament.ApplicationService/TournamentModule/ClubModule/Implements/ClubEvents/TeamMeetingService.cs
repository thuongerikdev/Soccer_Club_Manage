using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.Domain.Club.ClubEvent;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubEvent.ClubEvent;
using SM.Tournament.Dtos.ClubDto.ClubEvent.TeamMeetingEvent;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubEvents
{
    public class TeamMeetingService : TournamentServiceBase, IClubEventService
    {
        public TeamMeetingService(ILogger <TeamMeetingService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
    
        public async Task <TournamentResponeDto> CreateEvent(CreateEventDto createTeamMeetingEventDto)
        {
            try
            {
                var meeting = new TeamMeetingEvent
                {
                    ClubID = createTeamMeetingEventDto.ClubID,
                    EventName = createTeamMeetingEventDto.EventName,
                    EventDescription = createTeamMeetingEventDto.EventDescription,
                    EventDate = createTeamMeetingEventDto.EventDate,
                    EventLocation = createTeamMeetingEventDto.EventLocation,
                    EventStatus = createTeamMeetingEventDto.EventStatus,
                    membersCount = createTeamMeetingEventDto.membersCount,
                    MeetingAim = createTeamMeetingEventDto.MeetingAim,
                    MeetingContent = createTeamMeetingEventDto.MeetingContent,


                };
                _dbContext.TeamMeetingEvents.Add(meeting);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Team Meeting Event Success",
                    Data = meeting.EventID
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
        public async Task<TournamentResponeDto> UpdateEvent(UpdateEventDto updateTeamMeetingEventDto)
        {
            try
            {
                var meeting = _dbContext.TeamMeetingEvents.FirstOrDefault(x => x.EventID == updateTeamMeetingEventDto.EventID);
                if (meeting == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Event not found",
                        Data = null
                    };
                }
                meeting.EventName = updateTeamMeetingEventDto.EventName;
                meeting.EventDescription = updateTeamMeetingEventDto.EventDescription;
                meeting.EventDate = updateTeamMeetingEventDto.EventDate;
                meeting.EventLocation = updateTeamMeetingEventDto.EventLocation;
                meeting.EventStatus = updateTeamMeetingEventDto.EventStatus;
                meeting.membersCount = updateTeamMeetingEventDto.membersCount;
                meeting.MeetingAim = updateTeamMeetingEventDto.MeetingAim;
                meeting.MeetingContent = updateTeamMeetingEventDto.MeetingContent;

                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Team Meeting Event Success",
                    Data = meeting.EventID
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
                var meeting = _dbContext.TeamMeetingEvents.FirstOrDefault(x => x.EventID == eventID);
                if (meeting == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Event not found",
                        Data = null
                    };
                }
                _dbContext.TeamMeetingEvents.Remove(meeting);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Team Meeting Event Success",
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
                var meeting = _dbContext.TeamMeetingEvents.FirstOrDefault(x => x.EventID == eventID);
                if (meeting == null)
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
                    ErrorMessage = "Get Team Meeting Event Success",
                    Data = meeting
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
                var meetings = _dbContext.TeamMeetingEvents.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Team Meeting Event Success",
                    Data = meetings
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
                var meetings = _dbContext.TeamMeetingEvents.Where(x => x.ClubID == clubID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Team Meeting Event Success",
                    Data = meetings
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
