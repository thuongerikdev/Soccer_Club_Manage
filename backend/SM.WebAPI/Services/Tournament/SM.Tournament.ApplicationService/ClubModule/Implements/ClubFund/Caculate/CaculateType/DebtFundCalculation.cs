﻿using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.Caculate;
using SM.Tournament.Domain.Club.ClubFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Caculate.CaculateType
{
    public class DebtFundCalculation : IFundCalculationStrategy
    {
        public void CalculateFund(ClubFunds fund, decimal amount)
        {
            // Tính số tiền quỹ hiện tại bao gồm tiền đóng quỹ
            if (amount <= 0)
                throw new ArgumentException("Khoản nợ phải lớn hơn 0.");
            fund.Debt += amount;

        }
    }
}
