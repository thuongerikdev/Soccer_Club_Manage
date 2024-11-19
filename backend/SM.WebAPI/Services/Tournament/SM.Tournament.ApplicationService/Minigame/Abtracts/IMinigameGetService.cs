using SM.Tournament.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts
{
    public interface IMinigameGetService
    {

        public Task<TournamentResponeDto> GetMinigamesByTournament(int tournamentID);
        public Task<TournamentResponeDto> GetMinigamesByUser(int userID);
        public Task<TournamentResponeDto> GetMinigamesByMatch(int matchID);
        public Task<TournamentResponeDto> GetMinigamesByPrediction(int prediction);
        public Task<TournamentResponeDto> GetMinigamesByDate(DateTime date);
        public Task<TournamentResponeDto> GetMinigamesByType(string type);
        public Task<TournamentResponeDto> GetMinigamesByStatus(string status);
    }
}
