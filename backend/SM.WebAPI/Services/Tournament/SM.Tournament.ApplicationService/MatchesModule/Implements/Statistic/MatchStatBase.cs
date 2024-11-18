using SM.Tournament.Domain.Match;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic
{
    public class MatchStatBase
    {
        public TournamentResponeDto caculateStat (List <CaculateStatisticDto> stat )
        {
            var fouls = stat.Sum(x => x.Fouls);
            var yellowCard = stat.Sum(x => x.YellowCard);
            var redCard = stat.Sum(x => x.RedCard);

            var goals = stat.Sum(x => x.Goal);
            var assists = stat.Sum(x => x.Assist);
            var shots = stat.Sum(x => x.Shot);
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Player Statistics Success",
                Data = new
                {
                    fouls,
                    yellowCard,
                    redCard,
                    goals,
                    assists,
                    shots
                }
            };
        }
        public List<CaculateStatisticDto> MapToCaculateStatisticDto(List<MatchesStatistic> stats)
        {
            return stats.Select(x => new CaculateStatisticDto
            {
                Fouls = x.Fouls,
                YellowCard = x.YellowCard,
                RedCard = x.RedCard,
                Goal = x.Goal,
                Assist = x.Assist,
                Shot = x.Shot
            }).ToList();
        }
    }
}
