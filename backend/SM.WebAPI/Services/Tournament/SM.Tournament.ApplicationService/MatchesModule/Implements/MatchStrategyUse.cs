using Microsoft.Extensions.DependencyInjection;
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
            [FromKeyedServices("matches")] IMatchesStatisticStrategy matches,
            [FromKeyedServices("player")] IMatchesStatisticStrategy player,
            [FromKeyedServices("playerMatch")] IMatchesStatisticStrategy playerMatch,
            [FromKeyedServices("clubMatch")] IMatchesStatisticStrategy clubMatch,
            [FromKeyedServices("tournament")] IMatchesStatisticStrategy tournament,
            [FromKeyedServices("tournamentMatch")] IMatchesStatisticStrategy tournamentMatch,
            [FromKeyedServices("tournamentClub")] IMatchesStatisticStrategy tournamentClub)
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
                "matches" => _matches,
                "player" => _player,
                "playerMatch" => _playerMatch,
                "clubMatch" => _clubMatch,
                "tournament" => _tournament,
                "tournamentMatch" => _tournamentMatch,
                "tournamentClub" => _tournamentClub,
                _ => throw new ArgumentException("Invalid statistic type", nameof(type)),
            };
        }
    }
}