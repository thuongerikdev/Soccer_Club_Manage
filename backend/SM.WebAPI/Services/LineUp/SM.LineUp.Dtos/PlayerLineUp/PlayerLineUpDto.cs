using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Dtos.PlayerLineUp
{
    public class PlayerLineUpDto
    {
        public int LineUpId { get; set; }
        public int PlayerId { get; set; }
        public int ClubId { get; set; }
        public string PlayerPosition { get; set; }
        public bool IsCaptain { get; set; }
        public int PlayTime { get; set; }
    }
}
