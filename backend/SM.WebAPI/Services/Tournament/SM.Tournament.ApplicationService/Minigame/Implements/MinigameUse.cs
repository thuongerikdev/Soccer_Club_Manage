using Microsoft.Extensions.DependencyInjection;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Vote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements
{
    public  class MinigameUse : IMinigameUse
    {

        private readonly ICaculationResultStrategy _goal;
        private readonly ICaculationResultStrategy _pass;
        private readonly ICaculationResultStrategy _shot;
        private readonly ICaculationResultStrategy _fouls;
        private readonly ICaculateVote _vote;


        public MinigameUse(
            [FromKeyedServices("goal")] ICaculationResultStrategy goal,
            [FromKeyedServices("pass")] ICaculationResultStrategy pass,
            [FromKeyedServices("shot")] ICaculationResultStrategy shot,
            [FromKeyedServices("fouls")] ICaculationResultStrategy fouls,
            [FromKeyedServices("playerVote")] ICaculateVote vote



            )
        {
            
            _goal = goal;
            _pass = pass;
            _shot = shot;
            _fouls = fouls;
            _vote = vote;
        }
        public ICaculationResultStrategy chooseType (string type)
        {
            return type switch
            {
                "goal" => _goal,
                "pass" => _pass,
                "shot" => _shot,
                "fouls" => _fouls,
                //"vote" => _vote,
                _ => throw new ArgumentException("Invalid service type", nameof(type)),
            };
        }
        public ICaculateVote chooseVoteType (string type)
        {
            return type switch
            {
                "playerVote" => _vote,
                _ => throw new ArgumentException("Invalid service type", nameof(type)),
            };
        }
        
    }
}
