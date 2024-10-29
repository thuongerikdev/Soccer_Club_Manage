using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Players
{
    [Table(nameof(ClubPlayers))]
    public class ClubPlayers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        [MaxLength(50)]
        public string PlayerName { get; set; }
        [MaxLength(50)]
        public string PlayerPosition { get; set; }
        [MaxLength(50)]
        public string PlayerNationality { get; set; }
        [MaxLength(50)]
        public string PlayerImage { get; set; }
        public int ClubId { get; set; }
        public int PlayerAge { get; set; }
        public double PlayerValue { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerSkill { get; set; }
        public double PlayerSalary { get; set; }

    }
}
