using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Reward;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements
{
    public class RewardService : TournamentServiceBase , IRewardService
    {
        public RewardService (ILogger<RewardService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> createReward(CreateRewardDto createRewardDto)
        {
            var reward = new MinigameReward
            {
                RewardName = createRewardDto.RewardName,
                RewardDescription = createRewardDto.RewardDescription,
                RewardType = createRewardDto.RewardType,
                RewardValue = createRewardDto.RewardValue,
                createDate = createRewardDto.createDate,
                Rank = createRewardDto.Rank

            };

            _dbContext.MinigameRewards.Add(reward);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Create Reward Success",
                Data = null
            };
        }
        public async Task<TournamentResponeDto> deleteReward(int rewardID)
        {
            var reward = _dbContext.MinigameRewards.FirstOrDefault(x => x.MinigameRewardID == rewardID);
            if (reward == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Reward not found",
                    Data = null
                };
            }
            _dbContext.MinigameRewards.Remove(reward);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Delete Reward Success",
                Data = null
            };
        }
        public async Task<TournamentResponeDto> getReward(int rewardID)
        {
            var reward = _dbContext.MinigameRewards.FirstOrDefault(x => x.MinigameRewardID == rewardID);
            if (reward == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Reward not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Reward Success",
                Data = reward
            };
        }
        public async Task<TournamentResponeDto> getRewards()
        {
            var reward = _dbContext.MinigameRewards.ToList();
            if (reward == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Reward not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Reward Success",
                Data = reward
            };
        }
        public async Task<TournamentResponeDto> updateReward(UpdateRewardDto updateRewardDto)
        {
            var reward = _dbContext.MinigameRewards.FirstOrDefault(x => x.MinigameRewardID == updateRewardDto.MinigameRewardID);
            if (reward == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Reward not found",
                    Data = null
                };
            }
            reward.RewardName = updateRewardDto.RewardName;
            reward.RewardDescription = updateRewardDto.RewardDescription;
            reward.RewardType = updateRewardDto.RewardType;
            reward.RewardValue = updateRewardDto.RewardValue;
            reward.createDate = updateRewardDto.createDate;
            reward.Rank = updateRewardDto.Rank;
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Update Reward Success",
                Data = null
            };
        }

    }
}
