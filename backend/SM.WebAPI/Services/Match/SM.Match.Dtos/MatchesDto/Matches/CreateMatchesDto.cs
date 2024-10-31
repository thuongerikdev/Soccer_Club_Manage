using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.Dtos.MatchesDto.Matches
{
    public class CreateMatchesDto
    {
        public int MatchesName { get; set; }
        public string MatchesDescription { get; set; }
        public int TournamentId { get; set; }
        public string Stadium { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
