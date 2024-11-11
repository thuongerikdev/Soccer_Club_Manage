using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using SM.Tournament.Dtos.PlayerDto.PlayerFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund
{
    public interface ICaculateService
    {
        //public Task <TournamentResponeDto> GetFund(int fundID);
        //public Task<TournamentResponeDto> GetFundByClubID(int clubID);
    

        public Task<TournamentResponeDto> CaculateFunds(CreateActionFundDto createActionFundDto);


        //public Task<TournamentResponeDto> GetFundHistory(int clubID);
    }
}
