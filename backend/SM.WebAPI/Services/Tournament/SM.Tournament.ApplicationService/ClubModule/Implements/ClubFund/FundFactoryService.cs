using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.Caculate;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Caculate;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Caculate.CaculateType;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.ClubFundStatictic.Date;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.ClubFundStatictic.Rank;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.PlayerStatistic.AllFund;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic.PlayerStatistic.SpecificFund;
using SM.Tournament.Infrastructure;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund
{
    public class FundFactoryService
    {
        private readonly ILogger<PlayerFundStatistic> _playerfundstatistic;
        private readonly ILogger<PlayerFundTypeStatistic> _playerfundtypestatistic;

        private readonly ILogger<PlayerSpecificFundStatistic> _playerspecificfundstatistic;
        private readonly ILogger<PlayerSpecificTypeStatistic> _playerspecifictypestatistic;

        private readonly ILogger<PlayerFundRank> _playerfundrank;

        private readonly ILogger<YearStatistic> _yearstatistic;
        private readonly ILogger<DayStatistic> _daystatistic;
        private readonly ILogger<MonthStatistic> _monthstatistic;
        private readonly ILogger<WeekStatistic> _weekstatistic;


        private readonly TournamentDbContext _dbContext;

        public FundFactoryService(
          
            ILogger<PlayerFundTypeStatistic> playerfundtypestatistic,
            ILogger<PlayerSpecificTypeStatistic> playerspecifictypestatistic,

            ILogger<PlayerFundRank> playerfundrank,

            ILogger<YearStatistic> yearstatistic,
            ILogger<DayStatistic> daystatistic,
            ILogger<MonthStatistic> monthstatistic,
            ILogger<WeekStatistic> weekstatistic,

            TournamentDbContext dbContext)
        {
        
            _playerfundtypestatistic = playerfundtypestatistic;
            _playerspecifictypestatistic = playerspecifictypestatistic;

            _playerfundrank = playerfundrank;

            _yearstatistic = yearstatistic;
            _daystatistic = daystatistic;
            _monthstatistic = monthstatistic;
            _weekstatistic = weekstatistic;

            _dbContext = dbContext;
        }
        public IFundCalculationStrategy Create(string strategyType)
        {
        

            return strategyType switch
            {
                "Contribute" => new ContributeFundCaculation(),
                "Debt" => new DebtFundCalculation(),
                "Expense" => new ExpenseFundCalculation(),
                "ContributeTax" => new ContributeWithTax(),
                _ => throw new ArgumentException("Invalid strategy type", nameof(strategyType))
            };
        }
        public IFundStatisticStrategy CreateStatistic(string strategyType)
        {
            return strategyType switch
            {
                "PlayerFundType" => new PlayerFundTypeStatistic(_playerfundtypestatistic, _dbContext),
                "PlayerSpecificType" => new PlayerSpecificTypeStatistic(_playerspecifictypestatistic, _dbContext),

                "PlayerFundRank" => new PlayerFundRank(_playerfundrank, _dbContext),

                "Year" => new YearStatistic(_yearstatistic, _dbContext),
                "Day" => new DayStatistic(_daystatistic, _dbContext),
                "Month" => new MonthStatistic(_monthstatistic, _dbContext),
                "Week" => new WeekStatistic(_weekstatistic, _dbContext),

                _ => throw new ArgumentException("Invalid strategy type", nameof(strategyType))
            };
        }
    }
}