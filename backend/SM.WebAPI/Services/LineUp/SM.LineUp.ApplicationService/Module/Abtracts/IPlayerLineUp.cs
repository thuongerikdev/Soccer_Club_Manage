using SM.LineUp.Domain.LineUp;
using SM.LineUp.Dtos;
using SM.LineUp.Dtos.PlayerLineUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.ApplicationService.Module.Abtracts
{
    public interface IPlayerLineUp
    {
        public Task<LineUpResponeDto> CreatePlayerLineUps(List<PlayerLineUpDto> playerLineUpDtos);
        public Task<LineUpResponeDto> UpdatePlayerLineUp(UpdatePlayerLineUpDto updatePlayerLineUpDto);
        public Task<LineUpResponeDto> RemovePlayerLineUp(int playerLineUpId);
        public  ValueTask<LineUpResponeDto> GetAllPlayerLineUp();
        public ValueTask<LineUpResponeDto> GetPlayerLineUpById(int playerLineUpId);
    }
}
