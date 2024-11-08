using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.Dtos.LineUpDtos
{
    public class ReadLineUpDto
    {
        public int LineUpId { get; set; }
        public int MatchId { get; set; }
        public int ClubId { get; set; }
        public string LineUpName { get; set; }
        public string LineUpType { get; set; } // chế đọ : công khai / nội bộ / riêng tue
        public string MatchType { get; set; } // loại trận đấu : 1 - trận đấu thường, 2 - trận đấu tập huấn, 3 - trận đấu giao hữu

        public string StadiumBackGroud { get; set; } // loại sân : sân 5 người , sân 7 người 
        public DateTime CreateAt { get; set; }
    }
}
