using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SM.Tournament.Domain.Minigame.Predicts;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Infrastructure;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;

namespace SM.Tournament.ApplicationService.Minigame.Services
{
    public class ReceiveAwardService : IReceiveAwardService
    {
        private readonly TournamentDbContext _dbContext;

        public ReceiveAwardService(TournamentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PredictionRanking>> AwardRewardsAsync(int minigameID, List<Predictions> predictions)
        {
            // Fetch the minigame and validate existence
            var minigame = await _dbContext.Minigames
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MinigameID == minigameID);

            if (minigame == null)
                throw new Exception("Minigame not found");

            // Fetch rewards for the minigame
            var rewards = await _dbContext.MinigameRewards
                .AsNoTracking()
                .Where(x => x.RewardType == minigame.MinigameType)
                .OrderBy(x => x.Rank)
                .ToListAsync();

            if (!rewards.Any())
                throw new Exception("No rewards found");

            // Rank predictions by date
            var rankedPredictions = predictions
                .OrderBy(x => x.PredictionDate)
                .Select((x, index) => new PredictionRanking
                {
                    PredictionID = x.PredictionID,
                    MinigameID = minigameID,
                    UserID = x.UserID,
                    Rank = index + 1,
                    PredictionDate = x.PredictionDate
                })
                .ToList();

            foreach (var prediction in rankedPredictions)
            {
                // Find the reward based on rank and the reward type
                var reward = rewards
                    .FirstOrDefault(r => r.Rank == prediction.Rank); 

                if (reward != null)
                {
                    prediction.MinigameRewardID = reward.MinigameRewardID;
                }

                // Update PredictionTime for each PredictionRanking
                prediction.PredictionTime = DateTime.UtcNow; // Or any other value you want to set
            }

            // Add the prediction rankings to the database
            await _dbContext.PredictionRankings.AddRangeAsync(rankedPredictions);
            await _dbContext.SaveChangesAsync();

            // Return the ranked and rewarded predictions
            return rankedPredictions;
        }

    }
}
