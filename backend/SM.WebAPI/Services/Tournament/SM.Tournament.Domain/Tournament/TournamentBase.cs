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
    [Table(nameof(TournamentBase), Schema = DbSchema.Tournament)]
    public class TournamentBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TournamentID { get; set; }
        [MaxLength(50)]
        public int UserID { get; set; }
        public string TournamentName { get; set; }
        public decimal TournamentPrice { get; set; }
        [MaxLength(50)]
        public string TournamentDescription { get; set; }
        [MaxLength(50)]
        public string TournamentType { get;set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(50)]
        public string TournamentStatus { get; set; }
        [MaxLength(50)]
        public string TournamentLocation { get; set; }
        [MaxLength(50)]
        public string TournamentOrganizer { get; set; }
        [MaxLength(50)]
        public string TournamentContact { get; set; }
        public int numberMember { get; set; }

    }
  

    
}
