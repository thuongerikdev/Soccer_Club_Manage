using SM.Tournament.Dtos;
using SM.Tournament.Dtos.LineUpDto.LineUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.LineUpModule.Abtracts
{
    public interface ILineUpService
    {
        public Task<TournamentResponeDto> CreateLineUp(CreateLineUpDto createLineUpDto);
        public Task<TournamentResponeDto> UpdateLineUp(UpdateLineUpDto updateLineUpDto);
        public Task<TournamentResponeDto> DeleteLineUp(int lineUpId);
        public Task<TournamentResponeDto> GetAllLineUp();
        public Task<TournamentResponeDto> GetLineUpById(int lineUpId);
    }
}
