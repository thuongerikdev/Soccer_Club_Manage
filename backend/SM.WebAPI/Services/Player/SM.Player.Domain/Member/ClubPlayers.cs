using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Constant.Database;

namespace SM.Player.Domain.Players
{
    [Table(nameof(ClubPlayers), Schema = DbSchema.Player)]
    public class ClubPlayers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }
        [MaxLength(50)]
        public string PlayerName { get; set; }
        [MaxLength(50)]
        public string PlayerPosition { get; set; }
        [MaxLength(5000000)]
        public string PlayerImage { get; set; }
        public int ClubId { get; set; }
        public int PlayerAge { get; set; }
        public int Shirtnumber { get; set; }
        public int PlayerStatus { get; set; }
        [MaxLength(50)]
        public string leg { get; set; }
        public double height { get; set; }
        public double weight { get; set; }


    }
}
