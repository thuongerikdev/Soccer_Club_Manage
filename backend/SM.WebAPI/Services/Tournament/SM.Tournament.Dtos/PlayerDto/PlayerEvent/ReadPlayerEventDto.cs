using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.PlayerDto.PlayerEvent
{
    public class ReadPlayerEventDto
    {
        public int PlayerEventID { get; set; }
        public int PlayerID { get; set; }
        public int EventID { get; set; }
    }
}
