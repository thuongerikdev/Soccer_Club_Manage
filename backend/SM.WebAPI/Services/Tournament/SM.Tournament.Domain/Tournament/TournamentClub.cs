using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Tournament
{
    [Table(nameof(TournamentClub), Schema = DbSchema.Tournament)]
    public class TournamentClub
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TournamentClubID { get; set; }
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
