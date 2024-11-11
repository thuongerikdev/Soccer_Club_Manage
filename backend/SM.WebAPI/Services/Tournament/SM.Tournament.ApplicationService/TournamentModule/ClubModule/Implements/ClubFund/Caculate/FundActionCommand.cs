using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund;
using SM.Tournament.Domain.Club.ClubFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubFund.Caculate
{
    public class FundActionCommand
    {
        private readonly IFundCalculationStrategy _fundCalculationStrategy;
        private readonly ClubFunds _fund;
        private readonly decimal _amount;

        public FundActionCommand(IFundCalculationStrategy fundCalculationStrategy, ClubFunds fund, decimal amount)
        {
            _fundCalculationStrategy = fundCalculationStrategy;
            _fund = fund;
            _amount = amount;
        }

        public void CalculateFund()
        {
            _fundCalculationStrategy.CalculateFund(_fund, _amount);
        }
    }
}
