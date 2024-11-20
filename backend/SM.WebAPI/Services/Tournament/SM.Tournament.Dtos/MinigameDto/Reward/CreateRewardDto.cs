using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MinigameDto.Reward
{
    public class CreateRewardDto
    {
        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public string RewardType { get; set; }
        public int RewardValue { get; set; }
        public DateTime createDate { get; set; }
    }
}
