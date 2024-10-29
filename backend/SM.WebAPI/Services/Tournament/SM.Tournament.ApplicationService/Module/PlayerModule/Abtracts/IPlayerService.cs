using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto;
using SM.Tournament.Domain.Club;
using SM.Tournament.Dtos.PlayerDto;
using SM.Tournament.Domain.Players;

namespace SM.Tournament.ApplicationService.Module.PlayerModule.Abtracts
{
    public interface IPlayerService
    {
        public Task<TournamentResponeDto> CreatePlayer(CreatePlayerDto createPlayerDto);
        public Task<TournamentResponeDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto);
        public Task<TournamentResponeDto> DeletePlayer(int PlayerId);
        public ValueTask<IEnumerable<ClubPlayers>> GetAllPlayer();
        public ValueTask<TournamentResponeDto> GetPlayerById(int PlayerId);

    }
}
