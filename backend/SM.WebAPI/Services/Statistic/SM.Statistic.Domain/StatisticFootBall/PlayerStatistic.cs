using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Statistic.Domain.StatisticFootBall
{
    public class PlayerStatistic : StatisticBase
    {
        public int PlayerStatisticId { get; set; }
        public int Main_LineUp {  get; set; }
        public double AVGAge { get; set; }
        public int PlayerArchiveId { get; set; }
    }
}
