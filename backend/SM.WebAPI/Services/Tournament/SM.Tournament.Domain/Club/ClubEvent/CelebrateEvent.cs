using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Club.ClubEvent
{
    [Table(nameof(CelebrateEvent), Schema = DbSchema.Tournament)]
    public  class CelebrateEvent : ClubEventBase
    {
        public int ClubID { get; set; }


        [MaxLength(50)]
        public string Decor { get; set; }
        [MaxLength(50)]
        public string Menu { get; set; }
        [MaxLength(50)]
        public string Music { get; set; }
        [MaxLength(50)]
        public string minigame { get; set; }
    }
}
