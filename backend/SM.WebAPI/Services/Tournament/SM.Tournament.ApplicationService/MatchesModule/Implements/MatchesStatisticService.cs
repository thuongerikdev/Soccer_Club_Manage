using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.LineUpModule.Implements;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts;
using SM.Tournament.Domain.Match;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements
{
    public class MatchesStatisticService : TournamentServiceBase, IMatchesStatisticService
    {
        public MatchesStatisticService(ILogger<MatchesStatisticService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async Task<TournamentResponeDto> CreateMatchesStatistic(CreateMatchesStatisticDto createMatchesStatisticDto)
        {
            try
            {
                var matchesStatistic = new MatchesStatistic
                {
                    MatchesID = createMatchesStatisticDto.MatchesID,
                    LineUpID= createMatchesStatisticDto.LineUpID,
                    PlayerID = createMatchesStatisticDto.PlayerID,
                    ClubID = createMatchesStatisticDto.ClubID,
                    Goal = createMatchesStatisticDto.Goal,
                    Fouls = createMatchesStatisticDto.Fouls,
                    Pass = createMatchesStatisticDto.Pass,
                    RedCard = createMatchesStatisticDto.RedCard,
                    YellowCard = createMatchesStatisticDto.YellowCard,
                    Assist = createMatchesStatisticDto.Assist,
                    Shot = createMatchesStatisticDto.Shot


                };
                _dbContext.MatchesStatistics.Add(matchesStatistic);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Match Statistic Success",
                    Data = null
                };

            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> UpdateMatchesStatistic(UpdateMatchesStatisticDto updateMatchesStatisticDto)
        {
            try
            {
                var matchesStatistic = await _dbContext.MatchesStatistics.FindAsync(updateMatchesStatisticDto.MatchesID);
                if (matchesStatistic == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match Statistic Not Found",
                        Data = null
                    };
                }
                matchesStatistic.MatchesID = updateMatchesStatisticDto.MatchesID;
                matchesStatistic.PlayerID = updateMatchesStatisticDto.PlayerID;
                matchesStatistic.ClubID = updateMatchesStatisticDto.ClubID;
                matchesStatistic.Goal = updateMatchesStatisticDto.Goal;
                matchesStatistic.Fouls = updateMatchesStatisticDto.Fouls;
                matchesStatistic.Pass = updateMatchesStatisticDto.Pass;
                matchesStatistic.RedCard = updateMatchesStatisticDto.RedCard;
                matchesStatistic.YellowCard = updateMatchesStatisticDto.YellowCard;
                matchesStatistic.Assist = updateMatchesStatisticDto.Assist;
                matchesStatistic.Shot = updateMatchesStatisticDto.Shot;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Match Statistic Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> DeleteMatchesStatistic(int MatchesStatisticID)
        {
            try
            {
                var matchesStatistic = _dbContext.MatchesStatistics.Find(MatchesStatisticID);
                if (matchesStatistic == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match Statistic not found",
                        Data = null
                    };
                }
                _dbContext.MatchesStatistics.Remove(matchesStatistic);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Match Statistic Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetMatchesStatistic(int MatchesStatisticID)
        {
            try
            {
                var matchesStatistic = _dbContext.MatchesStatistics.Find(MatchesStatisticID);
                if (matchesStatistic == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match Statistic not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Match Statistic Success",
                    Data = matchesStatistic
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetMatchesStatistics()
        {
            try
            {
                var matchesStatistics = _dbContext.MatchesStatistics.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Match Statistic Success",
                    Data = matchesStatistics
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetClubMatches( int ClubID)
        {
            try
            {
                var matchesStatistics = _dbContext.MatchesStatistics.Where(x => x.ClubID == ClubID).ToList();

                var matchesID = matchesStatistics.GroupBy(x => x.MatchesID).Select(x => x.Key).ToList();

                var matches = _dbContext.Matches.Where(x => matchesID.Contains(x.MatchesID)).ToList();



                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Match Statistic Success",
                    Data = matches
                };

            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        } 
    }
}