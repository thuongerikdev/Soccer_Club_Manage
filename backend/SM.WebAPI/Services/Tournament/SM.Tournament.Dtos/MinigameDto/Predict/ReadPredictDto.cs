using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MinigameDto.Predict
{
    public class ReadPredictDto
    {
        public int PredictionID { get; set; }
        public int MinigameID { get; set; }
        public int MatchID { get; set; }
        public int UserID { get; set; }
        public int Prediction { get; set; }
        public DateTime PredictionDate { get; set; }
    }
}
