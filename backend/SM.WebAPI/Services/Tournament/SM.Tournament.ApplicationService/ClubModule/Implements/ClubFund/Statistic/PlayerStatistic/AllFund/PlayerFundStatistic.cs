using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.PlayerStatistic.AllFund
{
    public class PlayerFundStatistic : TournamentServiceBase, IFundStatisticStrategy
    {

        public PlayerFundStatistic(ILogger<PlayerFundStatistic> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> FundStatistic(ReadActionFundDto readActionFundDto)
        {
            var fundActionHistory = _dbContext.FundActionHistories.Where(x => x.PlayerID == readActionFundDto.PlayerID).ToList();
            var total = fundActionHistory.Sum(x => x.Amount);
            return new TournamentResponeDto
            {
                Data = total
            };
        }
    }
}

