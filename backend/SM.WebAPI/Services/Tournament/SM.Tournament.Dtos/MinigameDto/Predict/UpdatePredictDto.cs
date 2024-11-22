using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MinigameDto.Predict
{
    public class UpdatePredictDto
    {
        public int PredictionID { get; set; }
        public int MinigameID { get; set; }
        public int UserID { get; set; }
        public int TeamAscore { get; set; }
        public int TeamBscore { get; set; }
        public int PredictTotal { get; set; }
        public bool OddEven { get; set; }
        public int half { get; set; }
       public DateTime PredictionDate { get; set; }
    }
}
