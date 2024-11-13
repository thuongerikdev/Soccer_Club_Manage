using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic
{
    public class FundStatisticCommand
    {
        private readonly IFundStatisticStrategy _fundStatisticStrategy;
        private readonly ReadActionFundDto _readActionFundDto;
        public FundStatisticCommand(IFundStatisticStrategy fundStatisticStrategy, ReadActionFundDto readActionFundDto)
        {
            _fundStatisticStrategy = fundStatisticStrategy;
            _readActionFundDto = readActionFundDto;
        }
        public async Task<TournamentResponeDto> FundStatistic()
        {
            return await _fundStatisticStrategy.FundStatistic(_readActionFundDto);
        }

    }
}
