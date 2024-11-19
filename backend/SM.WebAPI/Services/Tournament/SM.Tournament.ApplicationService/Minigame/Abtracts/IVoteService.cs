using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts
{
    public interface IVoteService
    {
        public Task<TournamentResponeDto> CreateVote(CreateVoteDto createVoteDto);
        public Task<TournamentResponeDto> UpdateVote(UpdateVoteDto updateVoteDto);
        public Task<TournamentResponeDto> DeleteVote(int voteID);
        public Task<TournamentResponeDto> GetVotesByMinigame(int minigameID);
        public Task<TournamentResponeDto> GetVotebyID(int voteID);
        public Task<TournamentResponeDto> GetAllVotes();
    }
}
