using Microsoft.EntityFrameworkCore;
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

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.ClubFundStatictic.Date
{
    public class MonthStatistic : TournamentServiceBase, IFundStatisticStrategy
    {
        public MonthStatistic(ILogger<MonthStatistic> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async Task<TournamentResponeDto> FundStatistic(ReadActionFundDto readActionFundDto)
        {
            // Check if ActionDate has a value
            if (!readActionFundDto.ActionDate.HasValue)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "ActionDate is required.",
                    Data = null
                };
            }

            var actionMonth = readActionFundDto.ActionDate.Value.Month;

            var fundActionHistory = await _dbContext.FundActionHistories
                .Where(x => x.FundID == readActionFundDto.FundID
                             && x.FundActionType == readActionFundDto.FundActionType
                             && x.ActionDate.Month == actionMonth)
                .ToListAsync();

            var monthTotal = fundActionHistory.Sum(x => x.Amount);

            return new TournamentResponeDto
            {
                Data = monthTotal
            };
        }
    }
}

