using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MatchDto.MatchesStatistic
{
    namespace SM.Tournament.Dtos.MatchDto.MatchesStatistic
    {
        public class HalfStatisticDto
        {
            public CaculateStatisticDto TeamA { get; set; }
            public CaculateStatisticDto TeamB { get; set; }
        }

        public class MatchStatisticsDto
        {
            public HalfStatisticDto Half1 { get; set; }
            public HalfStatisticDto Half2 { get; set; }
        }
    }

}
