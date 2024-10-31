using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Statistic.Domain.StatisticFootBall
{
    public class ClubStatistic : StatisticBase
    {
        public int ClubStatisticId { get; set; }
   
        public int Player_Number { get; set; }
        public int ArchivementId { get; set; }
        public int matchWon { get; set; }
        public int matchDraw { get; set; }
        public int matchLost { get; set; }
    }
}
