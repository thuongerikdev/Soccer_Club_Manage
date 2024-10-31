using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.Domain.Matches
{
    public abstract class Statistic
    {
        public int ClubId { get; set; } // ID của câu lạc bộ
        public int RedCard { get; set; }
        public int YellowCard { get; set; }
        public int Goal { get; set; }
        public int Assist { get; set; }
        public int Penalty { get; set; }
        public int GoalAgainst { get; set; }
        public int CleanSheet { get; set; }
        public int OwnGoal { get; set; }
        public int PenaltySaved { get; set; }
        public int PenaltyMissed { get; set; }
        public int Shots { get; set; }
        public int ShotsOnTarget { get; set; }
        public int ShotsOffTarget { get; set; }
        public int Possession { get; set; }
        public int Passes { get; set; }
        public int PassesCompleted { get; set; }
        public int Tackles { get; set; }
    }
}
