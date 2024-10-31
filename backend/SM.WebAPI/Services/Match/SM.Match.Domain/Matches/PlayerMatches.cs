using SM.Match.Domain.Matches;

public class PlayerStatistic : Statistic
{
    public int PlayerStatisticId { get; set; } // Khóa chính cho thống kê cầu thủ
    public int PlayerId { get; set; }           // ID của cầu thủ
    public int MatchesId { get; set; }          // ID của trận đấu
    public int MinutesPlayed { get; set; }      // Thời gian thi đấu   
    public int ManOfTheMatch { get; set; }      // Cầu thủ xuất sắc nhất
    public int Dribbles { get; set; }           // Số lần đi bóng
    public int Interceptions { get; set; }       // Số lần cắt bóng
    public int Clearances { get; set; }          // Số lần phá bóng
    public int GoalsConceded { get; set; }      // Số bàn thua (đối với thủ môn)
}