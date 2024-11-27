using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Minigame.Predicts
{
    [Table(nameof(PredictionRanking), Schema = DbSchema.Tournament)]
    public class PredictionRanking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PredictionRankingID { get; set; }
        public int UserID { get; set; }
        public DateTime PredictionTime { get; set; }
        public int MinigameID { get; set; }
        public int Rank { get; set; }
        public DateTime PredictionDate { get; set; }
        public int PredictionID { get; set; }
        public int MinigameRewardID { get; set; }
    }

}
