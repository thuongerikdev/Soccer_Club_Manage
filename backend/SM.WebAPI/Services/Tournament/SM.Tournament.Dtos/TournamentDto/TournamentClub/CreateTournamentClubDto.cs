using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.TournamentDto.TournamentClub
{
    public  class CreateTournamentClubDto
    {
        public int TournamentID { get; set; }
        public int ClubID { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
    }
}
