using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Constant.Database;

namespace SM.Tournament.Domain.Match
{
    [Table(nameof(Matches), Schema = DbSchema.Tournament)]
    public class Matches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchesID { get; set; }
        public int MatchesName { get; set; }
        public int TeamA { get; set; } // CLubId
        public int TeamB { get; set; } //// CLubId
        [MaxLength(50)]
        public string MatchesDescription { get; set; }
        public int TournamentID { get; set; }
        [MaxLength(50)]
        public string Stadium { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }




    }
}
