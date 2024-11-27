using SM.Tournament.Domain.Minigame.Predicts;
using SM.Tournament.Domain.Minigame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation
{
    public  interface IReceiveAwardService
    {
        public Task<List<PredictionRanking>> AwardRewardsAsync(int minigameID, List<Predictions> predictions);
    }
}
