using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.TournamentStatistic
{
    public class TournamentStatistic
    {
        public int TournamentStatisticId { get; set; }
        public int TournamentId { get; set; }
        public int Rank { get; set; }
        public int ClubId { get; set; }

    }
}
