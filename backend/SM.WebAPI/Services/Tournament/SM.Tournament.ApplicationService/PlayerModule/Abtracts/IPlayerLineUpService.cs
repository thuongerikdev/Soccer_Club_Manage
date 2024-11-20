using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerLineUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.PlayerModule.Abtracts
{
    public interface IPlayerLineUpService
    {
        public Task<TournamentResponeDto> CreatePlayerLineUps(List<CreatePlayerLineUpDto> createPlayerLineUpDtos);
        public Task<TournamentResponeDto> UpdatePlayerLineUp(UpdatePlayerLineUpDto updatePlayerLineUpDto);
        public Task<TournamentResponeDto> DeletePlayerLineUp(int PlayerLineUpID);
        public Task<TournamentResponeDto> GetPlayerLineUp( );
        public Task<TournamentResponeDto> GetPlayerLineUpByLineUp( int LineUpID);
        public Task<TournamentResponeDto> DeletePlayerLineUpByPlayer(int PlayerID);

    }
}
