
using SM.Match.Dtos;
using SM.Match.Dtos.MatchesDto.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.ApplicationService.Module.MatchesModule.Abtracts
{
    public interface IMatchesService
    {
        public Task<MatchResponeDto> CreateMatches(CreateMatchesDto createMatchesDto);
        public Task<MatchResponeDto> UpdateMatches(UpdateMatchesDto updateMatchesDto);
        public Task<MatchResponeDto> RemoveMatches(int matchesId);
        public ValueTask<IEnumerable<GetMatchesDto>> GetAllMatches();
        public ValueTask<MatchResponeDto> GetMatchesById(int matchesId);
    }
}
