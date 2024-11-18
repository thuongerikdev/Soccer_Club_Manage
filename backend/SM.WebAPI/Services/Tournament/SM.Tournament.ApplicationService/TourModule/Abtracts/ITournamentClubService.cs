using SM.Tournament.Dtos;
using SM.Tournament.Dtos.TournamentDto.TournamentClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TourModule.Abtracts
{
    public interface ITournamentClubService
    {
        public Task<TournamentResponeDto> createTournamentClub(CreateTournamentClubDto createTournamentClubDto);
        public Task<TournamentResponeDto> updateTournamentClub(UpdateTournamentClubDto updateTournamentClubDto);
        public Task<TournamentResponeDto> deleteTournamentClub(int tournamentClubID);
        public Task<TournamentResponeDto> getTournamentClub(int tournamentClubID);
        public Task<TournamentResponeDto> getTournamentClubs();
    }
}
