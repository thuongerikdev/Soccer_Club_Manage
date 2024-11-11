﻿using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund;
using SM.Tournament.Domain.Club.ClubFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubFund.Caculate
{
    public class ExpenseFundCalculation : IFundCalculationStrategy
    {

        public void CalculateFund(ClubFunds fund, decimal amount)
        {

            if (amount <= 0)
                throw new ArgumentException("Khoản chi tiêu phải lớn hơn 0.");
            if (amount > fund.FundAmount)
                throw new InvalidOperationException("Không đủ tiền trong quỹ để chi tiêu.");

            fund.Expense += amount;
            fund.UpdateFundAmount(-amount);
        }
    }
}
