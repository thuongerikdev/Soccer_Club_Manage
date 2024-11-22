using SM.Tournament.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation
{
    public interface ICaculationResultStrategy
    {
        public Task <TournamentResponeDto> MinigameResult(string type ,int minigameID);
    }
}
