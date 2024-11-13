using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Domain.Tournament;
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
    public class ListPlayerOf1EventType : TournamentServiceBase, IEventStatisticStrategy
    {
        public ListPlayerOf1EventType(ILogger<ListPlayerOf1EventType> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> EventStatistic(ReadPlayerEventDto readPlayerEventDto)
        {
            try
            {
                var players = _dbContext.PlayerEvents.Where(x => x.EventType == readPlayerEventDto.EventType
                                                              && x.EventID == readPlayerEventDto.EventID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Player list Success",
                    Data = players
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
