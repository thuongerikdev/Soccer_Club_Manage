using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using SM.Tournament.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic
{
    public interface IFundStatisticService
    {
        public Task<TournamentResponeDto> FundStatistic(string strategyType, ReadActionFundDto readActionFundDto);
    }
}
