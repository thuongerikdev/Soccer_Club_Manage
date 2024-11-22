using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MinigameDto.Minigame
{
    public class CreateMinigameDto
    {
        public int TournamentID { get; set; }
        public int MatchesID { get; set; }
        public string MinigameType { get; set; }
        public DateTime StartDates { get; set; }
        public DateTime EndDates { get; set; }
        public int MinigameRewardID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Handicap { get; set; }

    }
}
