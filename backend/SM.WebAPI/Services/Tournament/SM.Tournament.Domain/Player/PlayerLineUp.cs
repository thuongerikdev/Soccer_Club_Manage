using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Player
{
    [Table(nameof(PlayerLineUp), Schema = DbSchema.Tournament)]
    public class PlayerLineUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerLineUpID { get; set; }
        public int PlayerID { get; set; }
        public int ClubID { get; set; }
        public int LineUpID { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(50)]
        public string Position { get; set; }
        public bool IsCaptain { get; set; }
    }
}
