using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Club.Dtos
{
    public class ClubResponeDto
    {
        public string EM { get; set; } // Error Message
        public int EC { get; set; }     // Error Code
        public object DT { get; set; }  // Data (Token or Array)
    }
}
