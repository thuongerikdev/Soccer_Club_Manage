﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.Dtos
{
    public class MatchResponeDto
    {
        public string EM { get; set; } // Error Message
        public int EC { get; set; }     // Error Code
        public object DT { get; set; }  // Data (Token or Array)
    }
}
