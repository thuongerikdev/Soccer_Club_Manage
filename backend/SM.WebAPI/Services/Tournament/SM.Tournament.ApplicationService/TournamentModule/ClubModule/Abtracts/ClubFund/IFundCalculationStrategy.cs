using SM.Tournament.Domain.Club.ClubFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund
{
    public interface  IFundCalculationStrategy
    {
        public void CalculateFund(ClubFunds fund, decimal amount);
    }
}
