using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.Dtos.MatchesDto.MatchesStatistic
{
    public class GetMatchesStatisticDto
    {
        public int MatchesStatisticId { get; set; }
        public int MatchesId { get; set; }
        public int ClubId { get; set; }
        public int redCard { get; set; }
        public int yellowCard { get; set; }
        public int goal { get; set; }
        public int assist { get; set; }
        public int penalty { get; set; }
        public int goalAgainst { get; set; }
        public int cleanSheet { get; set; }
        public int ownGoal { get; set; }
        public int penaltySaved { get; set; }
        public int penaltyMissed { get; set; }
        public int matchPlayed { get; set; }
        public int Corner { get; set; }
        public int Offside { get; set; }
        public int Fouls { get; set; }
        public int Shots { get; set; }
        public int ShotsOnTarget { get; set; }
        public int ShotsOffTarget { get; set; }
        public int Possession { get; set; }
        public int Passes { get; set; }
        public int PassesCompleted { get; set; }
        public int Tackles { get; set; }
    }
}
