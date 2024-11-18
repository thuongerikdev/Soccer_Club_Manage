using SM.Tournament.Dtos;
using SM.Tournament.Dtos.TournamentDto.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TourModule.Abtracts
{
    public interface ITournamentService
    {
        public Task<TournamentResponeDto> createTournament(CreateTournamentDto createTournamentDto);
        public Task<TournamentResponeDto> updateTournament(UpdateTournamentDto updateTournamentDto);
        public Task<TournamentResponeDto> deleteTournament(int tournamentID);
        public Task<TournamentResponeDto> getTournament(int tournamentID);
        public Task<TournamentResponeDto> getTournaments();

    }
}
