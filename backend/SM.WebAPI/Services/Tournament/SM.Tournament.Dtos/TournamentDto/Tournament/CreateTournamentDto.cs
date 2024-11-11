using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.TournamentDto.Tournament
{
    public  class CreateTournamentDto
    {
        public string TournamentName { get; set; }
        public string TournamentDescription { get; set; }
        public string TournamentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TournamentStatus { get; set; }
        public string TournamentLocation { get; set; }
        public string TournamentOrganizer { get; set; }
        public string TournamentContact { get; set; }
    }
}
