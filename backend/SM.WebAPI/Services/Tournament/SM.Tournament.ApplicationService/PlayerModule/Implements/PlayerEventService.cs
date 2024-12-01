using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.PlayerModule.Abtracts;
using SM.Tournament.Domain.Player;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerEvent;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.PlayerModule.Implements
{
    public class PlayerEventService : TournamentServiceBase , IPlayerEventService
    {
        public PlayerEventService(ILogger<PlayerEventService> logger , TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task <TournamentResponeDto> CreatePlayerEvent(CreatePlayerEventDto createPlayerEventDto)
        {
            try
            {
                var playerEvent = new PlayerEvent
                {
                    PlayerID = createPlayerEventDto.PlayerID,
                    EventID = createPlayerEventDto.EventID,
                    EventType = createPlayerEventDto.EventType,
                    JoinDate = createPlayerEventDto.JoinDate,
                    Position = createPlayerEventDto.Position
                };
                if (createPlayerEventDto.EventType == TourConst.Celebrate)
                {
                    var celebrate = await  _dbContext.CelebrateEvents.FirstOrDefaultAsync(x => x.EventID == createPlayerEventDto.EventID);
                    var clubPlayer = await _dbContext.ClubPlayers.FirstOrDefaultAsync(x => x.PlayerID == createPlayerEventDto.PlayerID);
                    if (celebrate.ClubID != clubPlayer.ClubID)
                    {
                        return new TournamentResponeDto
                        {
                            ErrorCode = 1,
                            ErrorMessage = "Event is not in this club",
                            Data = null
                        };
                    }
                }
                if (createPlayerEventDto.EventType == TourConst.Training)
                {
                    var training = await _dbContext.TrainingEvents.FirstOrDefaultAsync(x => x.EventID == createPlayerEventDto.EventID);
                    var clubPlayer = await _dbContext.ClubPlayers.FirstOrDefaultAsync(x => x.PlayerID == createPlayerEventDto.PlayerID);
                    if (training.ClubID !=  clubPlayer.ClubID)
                    {
                        return new TournamentResponeDto
                        {
                            ErrorCode = 1,
                            ErrorMessage = "Event is not in this club",
                            Data = null
                        };
                    }
                }
                if (createPlayerEventDto.EventType == TourConst.TeamMeeting)
                {
                    var teamMeeting = await _dbContext.TeamMeetingEvents.FirstOrDefaultAsync(x => x.EventID == createPlayerEventDto.EventID);
                    var clubPlayer = await _dbContext.ClubPlayers.FirstOrDefaultAsync(x => x.PlayerID == createPlayerEventDto.PlayerID);
                    if (teamMeeting.ClubID != clubPlayer.ClubID)
                    {
                        return new TournamentResponeDto
                        {
                            ErrorCode = 1,
                            ErrorMessage = "Event is not in this club",
                            Data = null
                        };
                    }
                }

                _dbContext.PlayerEvents.Add(playerEvent);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Player Event Success",
                    Data = playerEvent.EventID
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> UpdatePlayerEvent(UpdatePlayerEventDto updatePlayerEventDto)
        {
            try
            {
                var playerEvent = _dbContext.PlayerEvents.FirstOrDefault(x => x.PlayerEventID == updatePlayerEventDto.PlayerEventID);
                if (playerEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player Event Not Found",
                        Data = null
                    };
                }
                playerEvent.PlayerID = updatePlayerEventDto.PlayerID;
                playerEvent.EventID = updatePlayerEventDto.EventID;
                playerEvent.EventType = updatePlayerEventDto.EventType;
                playerEvent.JoinDate = updatePlayerEventDto.JoinDate;
                playerEvent.Position = updatePlayerEventDto.Position;
                if (updatePlayerEventDto.EventType == TourConst.Celebrate)
                {
                    var celebrate = await _dbContext.CelebrateEvents.FirstOrDefaultAsync(x => x.EventID == updatePlayerEventDto.EventID);
                    var clubPlayer = await _dbContext.ClubPlayers.FirstOrDefaultAsync(x => x.PlayerID == updatePlayerEventDto.PlayerID);
                    if (celebrate.ClubID != clubPlayer.ClubID)
                    {
                        return new TournamentResponeDto
                        {
                            ErrorCode = 1,
                            ErrorMessage = "Event is not in this club",
                            Data = null
                        };
                    }
                }
                if (updatePlayerEventDto.EventType == TourConst.Training)
                {
                    var training = await _dbContext.TrainingEvents.FirstOrDefaultAsync(x => x.EventID == updatePlayerEventDto.EventID);
                    var clubPlayer = await _dbContext.ClubPlayers.FirstOrDefaultAsync(x => x.PlayerID == updatePlayerEventDto.PlayerID);
                    if (training.ClubID != clubPlayer.ClubID)
                    {
                        return new TournamentResponeDto
                        {
                            ErrorCode = 1,
                            ErrorMessage = "Event is not in this club",
                            Data = null
                        };
                    }
                }
                if (updatePlayerEventDto.EventType == TourConst.TeamMeeting)
                {
                    var teamMeeting = await _dbContext.TeamMeetingEvents.FirstOrDefaultAsync(x => x.EventID == updatePlayerEventDto.EventID);
                    var clubPlayer = await _dbContext.ClubPlayers.FirstOrDefaultAsync(x => x.PlayerID == updatePlayerEventDto.PlayerID);
                    if (teamMeeting.ClubID != clubPlayer.ClubID)
                    {
                        return new TournamentResponeDto
                        {
                            ErrorCode = 1,
                            ErrorMessage = "Event is not in this club",
                            Data = null
                        };
                    }
                }
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Player Event Success",
                    Data = playerEvent.EventID
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> DeletePlayerEvent(int playerEventId)
        {
            try
            {
                var playerEvent = _dbContext.PlayerEvents.Find(playerEventId);
                if (playerEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player Event Not Found",
                        Data = null
                    };
                }
                _dbContext.PlayerEvents.Remove(playerEvent);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Player Event Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetAllPlayerEvent()
        {
            try
            {
                var playerEvents = _dbContext.PlayerEvents.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Player Event Success",
                    Data = playerEvents
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetPlayerEventById(int playerEventId)
        {
            try
            {
                var playerEvent = _dbContext.PlayerEvents.Find(playerEventId);
                if (playerEvent == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player Event Not Found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Player Event Success",
                    Data = playerEvent
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
    }
}
