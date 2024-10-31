using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Dtos.LineUpDtos
{
    public class ReadLineUpDto
    {
        public int LineUpId { get; set; }
        public int ClubId { get; set; }
        public int TournamentId { get; set; }
        public int MatchId { get; set; }
        public string formation { get; set; }
        public DateTime StartTime { get; set; }
    }
}
