using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Abtracts
{
    public interface IMatchesService
    {
        public Task<TournamentResponeDto> CreateMatches(CreateMatchesDto createMatchesDto);
        public Task<TournamentResponeDto> UpdateMatches(UpdateMatchesDto updateMatchesDto);
        public Task<TournamentResponeDto> DeleteMatches(int MatchID);
        public Task<TournamentResponeDto> GetMatches(int MatchID);
        public Task<TournamentResponeDto> GetAllMatches();

    }
}
