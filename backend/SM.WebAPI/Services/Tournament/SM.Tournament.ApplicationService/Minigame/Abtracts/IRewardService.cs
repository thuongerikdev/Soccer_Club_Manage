using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Reward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Abtracts
{
    public interface IRewardService
    {
        public Task<TournamentResponeDto> createReward(CreateRewardDto createRewardDto);
        public Task<TournamentResponeDto> updateReward(UpdateRewardDto updateRewardDto);
        public Task<TournamentResponeDto> deleteReward(int rewardID);
        public Task<TournamentResponeDto> getReward(int rewardID);
        public Task<TournamentResponeDto> getRewards();
    }
}
