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
    public class FundStatisticService : IFundStatisticService
    {

        private readonly FundFactoryService _fundactory;
        public FundStatisticService( FundFactoryService fundactory) { 
            _fundactory = fundactory;
        }
        public async Task<TournamentResponeDto> FundStatistic(string strategyType, ReadActionFundDto readActionFundDto)
        {
            var strategy = _fundactory.CreateStatistic(strategyType);
            var command =  new FundStatisticCommand(strategy ,readActionFundDto);
             return  await command.FundStatistic();
               
        }
    }
}
