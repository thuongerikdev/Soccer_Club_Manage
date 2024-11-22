using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Minigame
{
    [Table(nameof(Minigames), Schema = DbSchema.Tournament)]
    public class Minigames
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MinigameID { get; set; }
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
