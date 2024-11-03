using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Domain.LineUp
{
    public class LineUpBase
    {
        public int LineUpId { get; set; }
        public int ClubId { get; set; }
        public string LineUpName { get; set; }
        public int LineUpMember { get; set; } // số lượng cầu thủ 
        public int LineUpType { get; set; } // chế đọ : công khai / nội bộ / riêng tue

        public int MatchType { get; set; } // loại trận đấu : 1 - trận đấu thường, 2 - trận đấu tập huấn, 3 - trận đấu giao hữu
        public int TournamentId { get; set; }
        public int StadiumBackGroud { get; set; } // loại sân : sân 5 người , sân 7 người 
        public int MatchId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
