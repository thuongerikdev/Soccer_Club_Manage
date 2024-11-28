using SM.Tournament.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts.Vote
{
    public interface ICaculateVote
    {
        public Task<TournamentResponeDto> CaculateVote(int minigameID);
    }
}
