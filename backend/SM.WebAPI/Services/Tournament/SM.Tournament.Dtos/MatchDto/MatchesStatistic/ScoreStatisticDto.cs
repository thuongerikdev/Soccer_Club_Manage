using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MatchDto.MatchesStatistic
{
    public class ScoreStatisticDto
    {
        public CaculateStatisticDto TeamA { get; set; }
        public CaculateStatisticDto TeamB { get; set; }
    }
}
