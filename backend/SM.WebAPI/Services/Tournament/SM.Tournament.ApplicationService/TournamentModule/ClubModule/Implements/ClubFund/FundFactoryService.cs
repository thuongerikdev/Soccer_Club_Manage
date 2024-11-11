using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubFund.Caculate;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Implements.ClubFund.Factories
{
    public class FundFactoryService 
    {
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
    }
}