using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Domain.LineUp
{
    public class PlayerLineUp
    {
        public int PlayerLineUpId { get; set; }
        public int LineUpId { get; set; }
        public int PlayerId { get; set; }
        public int ClubId { get; set; }
        public int PlayerPosition { get; set; }
        public bool IsCaptain { get; set; }
        public int PlayTime { get; set; }
    }
}
