using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MinigameDto.Predict
{
    public class CreatePredictDto
    {
        // Thông tin về trận đấu và người chơi
        public int MinigameID { get; set; }        // ID của minigame
        public int MatchID { get; set; }           // ID của trận đấu
        public int UserID { get; set; }            // ID của người chơi
        public DateTime PredictionDate { get; set; } // Ngày dự đoán

        // Các thông tin liên quan đến các loại dự đoán
        public string HalfOrFull { get; set; }     // Chọn "Half" hoặc "Full" để xác định dự đoán cho hiệp hoặc cả trận
        public string PredictionType { get; set; } // Loại dự đoán: "MatchScore", "Total", "OddEven"

        // Các thông tin cần thiết cho từng loại dự đoán
        public int? TeamAscore { get; set; }       // Tỷ số đội A (dành cho loại "MatchScore")
        public int? TeamBscore { get; set; }       // Tỷ số đội B (dành cho loại "MatchScore")
        public int? PredictTotal { get; set; }     // Tổng số điểm hoặc tổng số bàn thắng (dành cho loại "Total")
        public bool? OddEven { get; set; }        // Chẵn lẻ (dành cho loại "OddEven")
        public int? half { get; set; }             // Hiệp đấu (dành cho loại "Half")



    }

}
