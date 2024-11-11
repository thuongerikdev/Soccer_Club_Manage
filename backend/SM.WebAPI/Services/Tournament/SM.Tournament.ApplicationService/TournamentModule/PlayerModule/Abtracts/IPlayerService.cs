using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.PlayerModule.Abtracts
{
    public interface IPlayerService
    {
        public Task<TournamentResponeDto> CreatePlayer(CreatePlayerDto createPlayerDto);
        public Task<TournamentResponeDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto);
        public Task<TournamentResponeDto> DeletePlayer(int playerId);
        public Task<TournamentResponeDto> GetAllPlayer();
        public Task<TournamentResponeDto> GetPlayerById(int playerId);
    }
}
