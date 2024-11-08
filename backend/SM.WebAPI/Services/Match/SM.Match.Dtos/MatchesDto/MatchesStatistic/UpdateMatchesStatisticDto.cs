using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.Dtos.MatchesDto.MatchesStatistic
{
    public class UpdateMatchesStatisticDto
    {
        public int MatchesStatisticId { get; set; } // Khóa chính cho thống kê trận đấu
        public int PlayerId { get; set; }
        public int LineUpId { get; set; }
        public int ClubId { get; set; }
        public int Score { get; set; }
        public int MatchesId { get; set; }           // ID của trận đấu
        public int Shot { get; set; }              // Số lần phạt góc
        public int Pass { get; set; }             // Số lần việt vị
        public int Fouls { get; set; }               // Số lỗi
    }
}
