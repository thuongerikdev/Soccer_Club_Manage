using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.PlayerDto.PlayerLineUp
{
    public  class ReadPlayerLineUpDto
    {
        public int PlayerLineUpID { get; set; }
        public int PlayerID { get; set; }
        public int LineUpID { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Position { get; set; }
        public bool IsCaptain { get; set; }
    }
}
