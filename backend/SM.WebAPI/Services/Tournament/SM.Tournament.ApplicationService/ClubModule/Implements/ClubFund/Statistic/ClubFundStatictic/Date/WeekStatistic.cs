﻿using Microsoft.EntityFrameworkCore;
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
            if (!readActionFundDto.ActionDate.HasValue)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "ActionDate is required.",
                    Data = null
                };
            }

            // Store the value of ActionDate to avoid multiple calls to Value
            var actionDate = readActionFundDto.ActionDate.Value;

            var fundActionHistory = await _dbContext.FundActionHistories
                .Where(x => x.FundID == readActionFundDto.FundID
                             && x.ActionDate.Year == actionDate.Year
                             && x.ActionDate.Month == actionDate.Month
                             && x.ActionDate.Day >= actionDate.Day - 7
                             && x.ActionDate.Day <= actionDate.Day)
                .ToListAsync();

            var weekTotal = fundActionHistory
                .Where(x => x.ActionDate.Year == actionDate.Year
                             && x.ActionDate.Month == actionDate.Month
                             && x.ActionDate.Day >= actionDate.Day - 7
                             && x.ActionDate.Day <= actionDate.Day)
                .Sum(x => x.Amount);

            return new TournamentResponeDto
            {
                Data = weekTotal
            };
        }
    }
}
