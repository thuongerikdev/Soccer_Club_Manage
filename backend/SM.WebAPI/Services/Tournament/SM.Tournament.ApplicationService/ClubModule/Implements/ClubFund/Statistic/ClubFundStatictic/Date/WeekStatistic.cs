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
    public class WeekStatistic : TournamentServiceBase, IFundStatisticStrategy
    {
        public WeekStatistic(ILogger<WeekStatistic> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async Task<TournamentResponeDto> FundStatistic(ReadActionFundDto readActionFundDto)
        {
            var fundActionHistory = await _dbContext.FundActionHistories.Where(x => x.FundID == readActionFundDto.FundID
                                                                               && x.ActionDate.Year == readActionFundDto.ActionDate.Year
                                                                               && x.ActionDate.Month == readActionFundDto.ActionDate.Month
                                                                               && x.ActionDate.Day >= readActionFundDto.ActionDate.Day - 7
                                                                               && x.ActionDate.Day <= readActionFundDto.ActionDate.Day).ToListAsync();

            var weektotal = fundActionHistory.Where(x => x.ActionDate.Year == readActionFundDto.ActionDate.Year
                                                      && x.ActionDate.Month == readActionFundDto.ActionDate.Month
                                                      && x.ActionDate.Day >= readActionFundDto.ActionDate.Day - 7
                                                      && x.ActionDate.Day <= readActionFundDto.ActionDate.Day).Sum(x => x.Amount);
            return new TournamentResponeDto
            {
                Data = weektotal
            };
        }
    }
}
