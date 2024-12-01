using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.Caculate;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;

using SM.Tournament.Infrastructure;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund
{
    public class FundStrategyUse : IFundStrategyUse
    {
        //private readonly IFundStatisticStrategy  _playerfundstatistic;
        private readonly IFundStatisticStrategy _playerfundtypestatistic;

        //private readonly IFundStatisticStrategy _playerspecificfundstatistic;
        private readonly IFundStatisticStrategy _playerspecifictypestatistic;

        private readonly IFundStatisticStrategy _playerfundrank;

        private readonly IFundStatisticStrategy _yearstatistic;
        private readonly IFundStatisticStrategy _daystatistic;
        private readonly IFundStatisticStrategy _monthstatistic;
        private readonly IFundStatisticStrategy _weekstatistic;

        private readonly IFundCalculationStrategy _contribute;
        private readonly IFundCalculationStrategy _debt;
        private readonly IFundCalculationStrategy _expense;
        private readonly IFundCalculationStrategy _contributeTax;


       
        public FundStrategyUse(

            [FromKeyedServices(TourConst.FundPlayerType)]  IFundStatisticStrategy playerfundtypestatistic,
            [FromKeyedServices(TourConst.FundPlayerSpecific)]  IFundStatisticStrategy playerspecifictypestatistic,

            [FromKeyedServices(TourConst.FundPlayerRank)]  IFundStatisticStrategy playerfundrank,

            [FromKeyedServices(TourConst.FundStatYear)] IFundStatisticStrategy yearstatistic,
            [FromKeyedServices(TourConst.FundStatDay)] IFundStatisticStrategy daystatistic,
            [FromKeyedServices(TourConst.FundStatMonth)] IFundStatisticStrategy monthstatistic,
            [FromKeyedServices(TourConst.FundStatWeek)] IFundStatisticStrategy weekstatistic,

            [FromKeyedServices(TourConst.FundContribute)] IFundCalculationStrategy contribute,
            [FromKeyedServices(TourConst.FundDebt)] IFundCalculationStrategy debt,
            [FromKeyedServices(TourConst.FundExpense)] IFundCalculationStrategy expense,
            [FromKeyedServices(TourConst.FundContributeTax)] IFundCalculationStrategy contributeTax

           )
        {
        
            _playerfundtypestatistic = playerfundtypestatistic;
            _playerspecifictypestatistic = playerspecifictypestatistic;

            _playerfundrank = playerfundrank;

            _yearstatistic = yearstatistic;
            _daystatistic = daystatistic;
            _monthstatistic = monthstatistic;
            _weekstatistic = weekstatistic;

            _contribute = contribute;
            _debt = debt;
            _expense = expense;
            _contributeTax = contributeTax;

         
        }
        public IFundCalculationStrategy Create(string strategyType)
        {
            return strategyType switch
            {
                TourConst.FundContribute => _contribute,
                TourConst.FundDebt => _debt,
                TourConst.FundExpense => _expense,
                TourConst.FundContributeTax => _contributeTax,
                _ => throw new ArgumentException("Invalid strategy type", nameof(strategyType))
            };
        }
        public IFundStatisticStrategy CreateStatistic(string strategyType)
        {
            return strategyType switch
            {
                TourConst.FundPlayerType => _playerfundtypestatistic,
                TourConst.FundPlayerSpecific => _playerspecifictypestatistic,

                TourConst.FundPlayerRank => _playerfundrank,

                TourConst.FundStatYear => _yearstatistic,
                TourConst.FundStatDay => _daystatistic,
                TourConst.FundStatMonth => _monthstatistic,
                TourConst.FundStatWeek => _weekstatistic,

                _ => throw new ArgumentException("Invalid strategy type", nameof(strategyType))
            };
        }
    }
}