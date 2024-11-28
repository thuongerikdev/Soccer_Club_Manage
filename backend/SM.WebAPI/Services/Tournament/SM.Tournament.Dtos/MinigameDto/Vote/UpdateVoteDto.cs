using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MinigameDto.Vote
{
    public class UpdateVoteDto
    {
        public int VoteID { get; set; }
        public int MinigameID { get; set; }
        public int MatchID { get; set; }
        public int UserID { get; set; }
        public int Selection { get; set; }
        public DateTime VoteDate { get; set; }
    }
}
