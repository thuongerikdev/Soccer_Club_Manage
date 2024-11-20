using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Minigame
{
    [Table(nameof(MinigameReward), Schema = DbSchema.Tournament)]
    public class MinigameReward
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MinigameRewardID { get; set; }
        public string RewardName { get; set; }
        public string RewardDescription { get; set; }
        public string RewardType {  get; set; }
        public int RewardValue { get; set; }
        public DateTime createDate { get; set; }
    }
}
