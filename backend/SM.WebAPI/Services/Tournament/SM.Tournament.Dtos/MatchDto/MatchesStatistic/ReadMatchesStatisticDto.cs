using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MatchDto.MatchesStatistic
{
    public class ReadMatchesStatisticDto
    {
        public int PlayerID { get; set; }
        public int ClubID { get; set; }
        public int MatchesID { get; set; }           // ID của trận đấu
        public int TournamentID { get; set; }
    }
}
