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
    [Table(nameof(PlayerEvent), Schema = DbSchema.Tournament)]
    public  class PlayerEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerEventID { get; set; }
        public int PlayerID { get; set; }
        public int EventID { get; set; }
    }
}
