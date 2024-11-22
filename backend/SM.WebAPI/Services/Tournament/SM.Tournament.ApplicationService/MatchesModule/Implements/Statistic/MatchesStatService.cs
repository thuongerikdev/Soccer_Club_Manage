using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Domain.Match;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic.SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements.Statistic.Match
{
    public class MatchesStatService : TournamentServiceBase, IMatchesStatisticStrategy
    {
        private readonly IMatchStatBase _matchStatBase;

        public MatchesStatService(ILogger<MatchesStatService> logger, TournamentDbContext dbContext, IMatchStatBase matchStatBase)
            : base(logger, dbContext)
        {
            _matchStatBase = matchStatBase;
        }

        public async Task<TournamentResponeDto> getStatisTic(ReadMatchesStatisticDto readMatchesStatisticDto)
        {
            var matchstat = await _dbContext.Matches.FirstOrDefaultAsync(m => m.MatchesID == readMatchesStatisticDto.MatchesID);
            if (matchstat == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Match not found",
                    Data = null
                };
            }

            // Lấy thống kê cho Hiệp 1
            var teamAStatsHalf1 = await _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID == matchstat.TeamA && x.half == 1)
                .ToListAsync();

            var teamBStatsHalf1 = await _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID == matchstat.TeamB && x.half == 1)
                .ToListAsync();

            // Lấy thống kê cho Hiệp 2
            var teamAStatsHalf2 = await _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID == matchstat.TeamA && x.half == 2)
                .ToListAsync();

            var teamBStatsHalf2 = await _dbContext.MatchesStatistics
                .Where(x => x.MatchesID == readMatchesStatisticDto.MatchesID && x.ClubID == matchstat.TeamB && x.half == 2)
                .ToListAsync();

            // Tính toán thống kê cho Hiệp 1
            var teamAStatsDtoHalf1 = _matchStatBase.MapToCaculateStatisticDto(teamAStatsHalf1);
            var teamBStatsDtoHalf1 = _matchStatBase.MapToCaculateStatisticDto(teamBStatsHalf1);
            var resultTeamAHalf1 = _matchStatBase.caculateStat(teamAStatsDtoHalf1);
            var resultTeamBHalf1 = _matchStatBase.caculateStat(teamBStatsDtoHalf1);

            // Tính toán thống kê cho Hiệp 2
            var teamAStatsDtoHalf2 = _matchStatBase.MapToCaculateStatisticDto(teamAStatsHalf2);
            var teamBStatsDtoHalf2 = _matchStatBase.MapToCaculateStatisticDto(teamBStatsHalf2);
            var resultTeamAHalf2 = _matchStatBase.caculateStat(teamAStatsDtoHalf2);
            var resultTeamBHalf2 = _matchStatBase.caculateStat(teamBStatsDtoHalf2);

            // Tạo đối tượng thống kê cho cả hai hiệp
            var matchStatistics = new MatchStatisticsDto
            {
                Half1 = new HalfStatisticDto
                {
                    TeamA = new CaculateStatisticDto
                    {
                        Goal = resultTeamAHalf1.Data is CaculateStatisticDto teamADataHalf1 ? teamADataHalf1.Goal : 0,
                        Pass = teamAStatsDtoHalf1.Sum(x => x.Pass),
                        Shot = teamAStatsDtoHalf1.Sum(x => x.Shot),
                        YellowCard = teamAStatsDtoHalf1.Sum(x => x.YellowCard),
                        RedCard = teamAStatsDtoHalf1.Sum(x => x.RedCard),
                        Fouls = teamAStatsDtoHalf1.Sum(x => x.Fouls)
                    },
                    TeamB = new CaculateStatisticDto
                    {
                        Goal = resultTeamBHalf1.Data is CaculateStatisticDto teamBDataHalf1 ? teamBDataHalf1.Goal : 0,
                        Pass = teamBStatsDtoHalf1.Sum(x => x.Pass),
                        Shot = teamBStatsDtoHalf1.Sum(x => x.Shot),
                        YellowCard = teamBStatsDtoHalf1.Sum(x => x.YellowCard),
                        RedCard = teamBStatsDtoHalf1.Sum(x => x.RedCard),
                        Fouls = teamBStatsDtoHalf1.Sum(x => x.Fouls)
                    }
                },
                Half2 = new HalfStatisticDto
                {
                    TeamA = new CaculateStatisticDto
                    {
                        Goal = resultTeamAHalf2.Data is CaculateStatisticDto teamADataHalf2 ? teamADataHalf2.Goal : 0,
                        Pass = teamAStatsDtoHalf2.Sum(x => x.Pass),
                        Shot = teamAStatsDtoHalf2.Sum(x => x.Shot),
                        YellowCard = teamAStatsDtoHalf2.Sum(x => x.YellowCard),
                        RedCard = teamAStatsDtoHalf2.Sum(x => x.RedCard),
                        Fouls = teamAStatsDtoHalf2.Sum(x => x.Fouls)
                    },
                    TeamB = new CaculateStatisticDto
                    {
                        Goal = resultTeamBHalf2.Data is CaculateStatisticDto teamBDataHalf2 ? teamBDataHalf2.Goal : 0,
                        Pass = teamBStatsDtoHalf2.Sum(x => x.Pass),
                        Shot = teamBStatsDtoHalf2.Sum(x => x.Shot),
                        YellowCard = teamBStatsDtoHalf2.Sum(x => x.YellowCard),
                        RedCard = teamBStatsDtoHalf2.Sum(x => x.RedCard),
                        Fouls = teamBStatsDtoHalf2.Sum(x => x.Fouls)
                    }
                }
            };

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Match Statistics Success",
                Data = matchStatistics
            };
        }
    }
}