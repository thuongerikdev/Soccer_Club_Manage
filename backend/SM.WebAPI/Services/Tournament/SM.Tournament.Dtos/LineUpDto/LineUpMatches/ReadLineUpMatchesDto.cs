using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.LineUpDto.LineUpMatches
{
    public class ReadLineUpMatchesDto
    {
        public int LineUpMatchesID { get; set; }
        public int LineUpID { get; set; }
        public int MatchID { get; set; }
    }
}
