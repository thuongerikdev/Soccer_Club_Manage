using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Domain.LineUp
{
    [Table(nameof(PlayerLineUp))]
    public class PlayerLineUp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerLineUpId { get; set; }
        public int LineUpId { get; set; }
        public int PlayerId { get; set; }
        public int ClubId { get; set; }
        [MaxLength(50)]
        public string PlayerPosition { get; set; }
        public bool IsCaptain { get; set; }
        public int PlayTime { get; set; }
    }
}
