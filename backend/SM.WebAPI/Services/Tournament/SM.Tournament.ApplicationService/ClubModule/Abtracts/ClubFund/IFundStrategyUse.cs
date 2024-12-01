using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.Caculate;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund
{
    public interface IFundStrategyUse
    {
        public IFundStatisticStrategy CreateStatistic(string strategyType);
        public IFundCalculationStrategy Create(string strategyType);
    }
}
