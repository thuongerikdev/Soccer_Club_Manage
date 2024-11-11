using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerFund;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.PlayerModule.Abtracts
{
    public  interface  IPlayerFundService 
    {
        public Task<TournamentResponeDto> CreatePlayerFund (CreatePlayerFundDto createPlayerFundDto);
        public Task<TournamentResponeDto> UpdatePlayerFund(UpdatePlayerFundDto updatePlayerFundDto);
        public Task<TournamentResponeDto> DeletePlayerFund(int playerFundId);
        public Task<TournamentResponeDto> GetAllPlayerFund();
        public Task <TournamentResponeDto> GetPlayerFundById(int playerFundId);
    }
}
