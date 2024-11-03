using SM.Match.Domain.Statistic;

public class MatchStatistic : Statistic
{
    public int MatchesStatisticId { get; set; } // Khóa chính cho thống kê trận đấu
    public int MatchesId { get; set; }           // ID của trận đấu
    public int Corner { get; set; }              // Số lần phạt góc
    public int Offside { get; set; }             // Số lần việt vị
    public int Fouls { get; set; }               // Số lỗi
    public int MatchPlayed { get; set; }         // Số trận đã chơi
}