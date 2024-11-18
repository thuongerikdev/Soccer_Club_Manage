using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.MatchDto.MatchesStatistic
{
    public class CaculateStatisticDto
    {
        public int Shot { get; set; }              // Số lần phạt góc
        public int Pass { get; set; }             // Số lần việt vị
        public int Fouls { get; set; }               // Số lỗi
        public int RedCard { get; set; }            // Số thẻ đỏ
        public int YellowCard { get; set; }         // Số thẻ vàng
        public int Assist { get; set; }             // Số lần kiến tạo
        public int Goal { get; set; }               // Số bàn thắng
    }
}
