using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MinigameDto.Minigame
{
    public class ReadMinigameDto
    {
        public int MinigameID { get; set; }
        public int TournamentID { get; set; }
        public string MinigameType { get; set; }
        public DateTime StartDates { get; set; }
        public DateTime EndDates { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
