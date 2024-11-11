using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MatchDto.MatchesStatistic
{
    public  class CreateMatchesStatisticDto
    {
        public int PlayerID { get; set; }
        public int LineUpID { get; set; }
        public int ClubID { get; set; }
        public int Score { get; set; }
        public int MatchesID { get; set; }           // ID của trận đấu
        public int Shot { get; set; }              // Số lần phạt góc
        public int Pass { get; set; }             // Số lần việt vị
        public int Fouls { get; set; }               // Số lỗi
    }
}
