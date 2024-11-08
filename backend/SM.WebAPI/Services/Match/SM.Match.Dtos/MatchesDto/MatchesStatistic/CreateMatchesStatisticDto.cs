

namespace SM.Match.Dtos.MatchesDto.MatchesStatistic
{
    public class CreateMatchesStatisticDto
    {
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
