using SM.Tournament.Domain.Club.ClubFund;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic
{
    public interface IFundStatisticStrategy

    {
        public Task<TournamentResponeDto> FundStatistic( ReadActionFundDto readActionFundDto) ;
    }
}
