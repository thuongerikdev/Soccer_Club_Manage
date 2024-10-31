using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.Domain.Matches
{
    public  class Matches
    {
        public int MatchesId { get; set; }
        public int MatchesName { get; set; }
        public string MatchesDescription { get; set; }
        public int TournamentId { get; set; }
        public string Stadium { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeamWin { get; set; }
        public int TeamLose { get; set; }

        

    }
}
