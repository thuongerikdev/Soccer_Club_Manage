using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain
{
    public class TournamentMatchSchedule
    {
        public int TournamentMatchScheduleId { get; set; }
        public int TournamentId { get; set; }
        public int MatchId { get; set; }
        public int Round { get; set; }
        public DateTime MatchDate { get; set; }
     

    }
}
