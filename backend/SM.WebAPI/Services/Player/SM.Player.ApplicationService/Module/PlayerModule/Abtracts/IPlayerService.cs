using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Player.Domain.Players;
using SM.Player.Dtos;
using SM.Player.Dtos.PlayerDto;

namespace SM.Player.ApplicationService.Module.PlayerModule.Abtracts
{
    public interface IPlayerService
    {
        public Task<PlayerResponeDto> CreatePlayer(CreatePlayerDto createPlayerDto);
        public Task<PlayerResponeDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto);
        public Task<PlayerResponeDto> DeletePlayer(int PlayerId);
        public ValueTask<PlayerResponeDto> GetAllPlayer();
        public ValueTask<PlayerResponeDto> GetPlayerById(int PlayerId);

    }
}
