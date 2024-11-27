using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MatchDto
{
    public class MatchDto
    {
        public int TeamA { get; set; }
        public int TeamB { get; set; }
        public int TournamentID { get; set; }
        public int Round { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Stadium { get; set; }
        public string MatchesName { get; set; }
        public string MatchesDescription { get; set; }
        public bool IsFinish { get; set; }
    }
}
