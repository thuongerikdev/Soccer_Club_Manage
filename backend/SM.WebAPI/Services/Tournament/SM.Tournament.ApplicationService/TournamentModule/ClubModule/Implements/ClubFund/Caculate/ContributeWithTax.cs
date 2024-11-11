﻿using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund;
using SM.Tournament.Domain.Club.ClubFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubFund.Caculate
{
    public class ContributeWithTax : IFundCalculationStrategy
    {
        public void CalculateFund(ClubFunds fund, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Khoản đóng quỹ phải lớn hơn 0.");
            decimal fundwithtax = amount * 0.9m;
            fund.Contribution += fundwithtax;
            fund.FundAmount += fundwithtax;
            //fund.UpdateFundAmount(amount);
        }
    }
}
