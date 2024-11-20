using Microsoft.Extensions.DependencyInjection;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements
{
    public  class MinigameUse : IMinigameUse
    {
        private readonly ICaculationResultStrategy _matchesScore;
        private readonly ICaculationResultStrategy _numberOfFouls;
        private readonly ICaculationResultStrategy _numberOfPass;
        private readonly ICaculationResultStrategy _numberOfShot;
        private readonly ICaculationResultStrategy _playerVote;
        public MinigameUse(
            [FromKeyedServices("matchesScore")] ICaculationResultStrategy matchesScore,
            [FromKeyedServices("numberOfFouls")] ICaculationResultStrategy numberOfFouls,
            [FromKeyedServices("numberOfPass")] ICaculationResultStrategy numberOfPass,
            [FromKeyedServices("numberOfShot")] ICaculationResultStrategy numberOfShot,
            [FromKeyedServices("playerVote")] ICaculationResultStrategy playerVote)
        {
            _matchesScore = matchesScore;
            _numberOfFouls = numberOfFouls;
            _numberOfPass = numberOfPass;
            _numberOfShot = numberOfShot;
            _playerVote = playerVote;
        }
        public ICaculationResultStrategy chooseType (string type)
        {
            return type switch
            {
                "MatchesScore" => _matchesScore,
                "NumberOfFouls" => _numberOfFouls,
                "NumberOfPass" => _numberOfPass,
                "NumberOfShot" => _numberOfShot,
                "PlayerVote" => _playerVote,
                _ => throw new ArgumentException("Invalid service type", nameof(type)),
            };
        }
    }
}
