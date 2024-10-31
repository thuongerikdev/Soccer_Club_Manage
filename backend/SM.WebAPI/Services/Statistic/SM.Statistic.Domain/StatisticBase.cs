using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Statistic.Domain
{
    public class StatisticBase
    {
        public int StatisticId { get; set; }
        public int ClubId { get; set; }
        public int TournamentId { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int LineUpId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string season { get; set; }

    }
}
