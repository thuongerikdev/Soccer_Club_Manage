using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Abtracts
{
    public interface IMatchesStatisticService
    {
        public Task <TournamentResponeDto> CreateMatchesStatistic(CreateMatchesStatisticDto createMatchesStatisticDto);
        public Task<TournamentResponeDto> UpdateMatchesStatistic(UpdateMatchesStatisticDto updateMatchesStatisticDto);
        public Task<TournamentResponeDto> DeleteMatchesStatistic(int matchesStatisticID);
        public Task<TournamentResponeDto> GetMatchesStatistic(int matchesStatisticID);
        public Task<TournamentResponeDto> GetMatchesStatistics();
        public Task<TournamentResponeDto> GetClubMatches( int ClubID);
     
    }
}
