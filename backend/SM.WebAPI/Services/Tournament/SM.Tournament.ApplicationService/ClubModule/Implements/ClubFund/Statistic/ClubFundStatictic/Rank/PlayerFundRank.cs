using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.ClubFundStatictic.Date;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.ClubFundStatictic.Rank
{
    public class PlayerFundRank : TournamentServiceBase , IFundStatisticStrategy
    {
        public PlayerFundRank(ILogger<PlayerFundRank> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> FundStatistic(ReadActionFundDto readActionFundDto)
        {
            var fundActionHistory = await _dbContext.FundActionHistories.Where(x => x.FundID == readActionFundDto.FundID
                                                                                 && x.FundActionType == readActionFundDto.FundActionType).ToListAsync();
            var playerFundRank = fundActionHistory.GroupBy(x => x.PlayerID).Select(x => new { PlayerID = x.Key, Total = x.Sum(y => y.Amount) }).OrderByDescending(x => x.Total).ToList();
            return new TournamentResponeDto
            {
                Data = playerFundRank
            };
        }
    }
}
