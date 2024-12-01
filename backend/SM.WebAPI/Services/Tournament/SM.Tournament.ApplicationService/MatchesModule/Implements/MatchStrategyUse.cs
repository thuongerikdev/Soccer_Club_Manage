using Microsoft.Extensions.DependencyInjection;
using SM.Constant.Tournament;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using System;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements
{
    public class MatchStrategyUse : IMatchStatisticUse
    {
        private readonly IMatchesStatisticStrategy _matches;
        private readonly IMatchesStatisticStrategy _player;
        private readonly IMatchesStatisticStrategy _playerMatch;
        private readonly IMatchesStatisticStrategy _clubMatch;
        private readonly IMatchesStatisticStrategy _tournament;
        private readonly IMatchesStatisticStrategy _tournamentMatch;
        private readonly IMatchesStatisticStrategy _tournamentClub;

        public MatchStrategyUse(
            [FromKeyedServices(TourConst.MatchStat)] IMatchesStatisticStrategy matches,
            [FromKeyedServices(TourConst.MatchPlayerStat)] IMatchesStatisticStrategy player,
            [FromKeyedServices(TourConst.MatchPlayerMatchStat)] IMatchesStatisticStrategy playerMatch,
            [FromKeyedServices(TourConst.MatchClubStat)] IMatchesStatisticStrategy clubMatch,
            [FromKeyedServices(TourConst.MatchTournamentStat)] IMatchesStatisticStrategy tournament,
            [FromKeyedServices(TourConst.MatchTournamentMatchStat)] IMatchesStatisticStrategy tournamentMatch,
            [FromKeyedServices(TourConst.MatchTournamentClubStat)] IMatchesStatisticStrategy tournamentClub)
        {
            _matches = matches;
            _player = player;
            _playerMatch = playerMatch;
            _clubMatch = clubMatch;
            _tournament = tournament;
            _tournamentMatch = tournamentMatch;
            _tournamentClub = tournamentClub;
        }

        public IMatchesStatisticStrategy ChooseStatistic(string type)
        {
            return type switch
            {
                TourConst.MatchStat => _matches,
                TourConst.MatchPlayerStat => _player,
                TourConst.MatchPlayerMatchStat => _playerMatch,
                TourConst.MatchClubStat => _clubMatch,
                TourConst.MatchTournamentStat => _tournament,
                TourConst.MatchTournamentMatchStat => _tournamentMatch,
                TourConst.MatchTournamentClubStat => _tournamentClub,
                _ => throw new ArgumentException("Invalid statistic type", nameof(type)),
            };
        }
    }
}