using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.PlayerModule.Abtracts
{
    public interface IPlayerEventService
    {
        public Task<TournamentResponeDto> CreatePlayerEvent(CreatePlayerEventDto createPlayerEventDto);
        public Task<TournamentResponeDto> UpdatePlayerEvent(UpdatePlayerEventDto updatePlayerEventDto);
        public Task<TournamentResponeDto> DeletePlayerEvent(int playerEventId);
        public Task<TournamentResponeDto> GetAllPlayerEvent();
        public Task<TournamentResponeDto> GetPlayerEventById(int playerEventId);
    }
}
