using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Constant.Tournament
{
    public static class TourConst
    {
        //Event
        public const string Celebrate = "celebrate";
        public const string Training = "training";
        public const string TeamMeeting = "teamMeeting";
        public const string EventPlayer  = "eventPlayer";
        public const string EventPlayerType = "eventPlayerType";

        //Fund
        public const string FundContribute = "contribute";
        public const string FundContributeTax = "contributeTax";
        public const string FundDebt = "debt";
        public const string FundExpense = "expense";
        public const string FundPlayerType = "fundPlayerTotal";
        public const string FundPlayerSpecific = "fundPlayerTotalType";
        public const string FundPlayerRank = "fundPlayerRank";
        public const string FundStatYear = "year";
        public const string FundStatMonth = "month";
        public const string FundStatWeek = "week";
        public const string FundStatDay = "day";

        //Prediction
        public const string HalfOrFullTime = "halfOrFullTime";
        public const string PredictType = "predictType";
        //half
        public const string H1 = "H1";
        public const string H2 = "H2";
        public const string Full = "full";
        //predictType
        public const string MatchScore = "matchScore";
        public const string OddEven = "oddEven";
        public const string Total = "total";
        //Team
        public const string TeamA = "teamA";
        public const string TeamB = "teamB";
        //predict topic
        public const string PredictGoal = "goal";
        public const string PredictPass = "pass";
        public const string PredictShot = "shot";
        public const string PredictFoul = "foul";
        public const string PlayerVote = "playerVote";
        //MinigameType
        public const string Vote = "vote";
        public const string Predict = "predict";




        // MatchesStat
        public const string MatchStat = "matches";
        public const string MatchClubStat = "matchClub";
        public const string MatchPlayerStat = "player";
        public const string MatchPlayerMatchStat = "playerMatch";
        public const string MatchTournamentStat = "tournament";
        public const string MatchTournamentClubStat = "tournamentClub";
        public const string MatchTournamentMatchStat = "tournamentMatch";
    }
}
