using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Domain.Player;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerEvent;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents.Statistic
{
    public class ListEventOfPlayer: TournamentServiceBase , IEventStatisticStrategy
    {
        public ListEventOfPlayer (ILogger<ListEventOfPlayer> logger , TournamentDbContext dbContext ) : base(logger , dbContext)
        {
        }
        public async Task<TournamentResponeDto> EventStatistic(ReadPlayerEventDto readPlayerEventDto)
        {
            try
            {
                var players = _dbContext.PlayerEvents.Where(x => x.PlayerID == readPlayerEventDto.PlayerID
                                  && x.EventType == readPlayerEventDto.EventType).ToList()
                                  .Select(pe => pe.EventID);

                if(readPlayerEventDto.EventType == "TeamMeeting")
                {
                    var teamMeetings = _dbContext.TeamMeetingEvents.Where(x => players.Contains(x.EventID)).ToList();
                    return new TournamentResponeDto
                    {
                        ErrorCode = 0,
                        ErrorMessage = "Get Player list Success",
                        Data = teamMeetings
                    };
                }
                if (readPlayerEventDto.EventType == "Training")
                {
                    var trainings = _dbContext.TrainingEvents.Where(x => players.Contains(x.EventID)).ToList();
                    return new TournamentResponeDto
                    {
                        ErrorCode = 0,
                        ErrorMessage = "Get Player list Success",
                        Data = trainings
                    };
                }
                if (readPlayerEventDto.EventType == "Match")
                {
                    var matches = _dbContext.TrainingEvents.Where(x => players.Contains(x.EventID)).ToList();
                    return new TournamentResponeDto
                    {
                        ErrorCode = 0,
                        ErrorMessage = "Get Player list Success",
                        Data = matches
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Invalid EventType",
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
    }
}
