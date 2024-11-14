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
    public class YearStatistic : TournamentServiceBase, IFundStatisticStrategy
    {
        public YearStatistic(ILogger<YearStatistic> logger, TournamentDbContext dbContext) : base(logger, dbContext)
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

            var actionDate = readActionFundDto.ActionDate.Value;

            var fundActionHistory = await _dbContext.FundActionHistories
                .Where(x => x.FundID == readActionFundDto.FundID
                             && x.FundActionType == readActionFundDto.FundActionType
                             && x.ActionDate.Year == actionDate.Year)
                .ToListAsync();

            var yearTotal = fundActionHistory.Sum(x => x.Amount);

            return new TournamentResponeDto
            {
                Data = yearTotal
            };
        }
    }
}
