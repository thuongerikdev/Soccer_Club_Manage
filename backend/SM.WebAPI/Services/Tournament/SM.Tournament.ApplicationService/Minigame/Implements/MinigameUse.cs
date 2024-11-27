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

        private readonly ICaculationResultStrategy _goal;
        private readonly ICaculationResultStrategy _pass;
        private readonly ICaculationResultStrategy _shot;
        private readonly ICaculationResultStrategy _fouls;


        public MinigameUse(
            [FromKeyedServices("goal")] ICaculationResultStrategy goal,
            [FromKeyedServices("pass")] ICaculationResultStrategy pass,
            [FromKeyedServices("shot")] ICaculationResultStrategy shot,
            [FromKeyedServices("fouls")] ICaculationResultStrategy fouls



            )
        {
            
            _goal = goal;
            _pass = pass;
            _shot = shot;
            _fouls = fouls;
        }
        public ICaculationResultStrategy chooseType (string type)
        {
            return type switch
            {
                "goal" => _goal,
                "pass" => _pass,
                "shot" => _shot,
                "fouls" => _fouls,
                _ => throw new ArgumentException("Invalid service type", nameof(type)),
            };
        }
    }
}
