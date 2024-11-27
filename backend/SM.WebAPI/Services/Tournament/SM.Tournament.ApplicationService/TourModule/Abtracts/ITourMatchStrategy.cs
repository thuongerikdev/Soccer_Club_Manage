using SM.Tournament.Domain.Match;
using SM.Tournament.Domain.Tournament;
using SM.Tournament.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TourModule.Abtracts
{
    public interface ITourMatchStrategy

    {
        public  Task<TournamentResponeDto> CreateTournamentMatch(int tournamentID);
        public  Task<TournamentResponeDto> ProcessMatchResult(int matchID);
        public Task<TournamentResponeDto> GetStandings(int TournamentID);

        public  Task<TournamentResponeDto> CreateSemiFinalMatches(int TournamentID);

        public Task<TournamentResponeDto> CreateFinalAndThirdPlaceMatches(int TournamentID);
        public  Task<TournamentResponeDto> DetermineFinalRankings(int TournamentID);
    }
}
