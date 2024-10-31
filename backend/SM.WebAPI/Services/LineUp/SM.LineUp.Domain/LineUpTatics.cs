using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Domain
{
    public  class LineUpTatics
    {
        public int LineUpTaticsId { get; set; }
        public int LineUpId { get; set; }
        public string strategy { get; set; }
        public string strategyDescription { get; set; }
        public string StartTatics { get; set; }
        public string StartTaticsDescription { get;set; }
    }
}
