using Microsoft.EntityFrameworkCore;
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
                    Shot = createMatchesStatisticDto.Shot,
                    half = createMatchesStatisticDto.half


                };
                var LineUp = await _dbContext.LineUps.FirstOrDefaultAsync(x => x.LineUpID == createMatchesStatisticDto.LineUpID
                                                                            && x.ClubID == matchesStatistic.ClubID);
                var player = await _dbContext.PlayerLineUps.FirstOrDefaultAsync(x => x.PlayerID == createMatchesStatisticDto.PlayerID 
                                                                            && x.LineUpID == createMatchesStatisticDto.LineUpID);
                var club = await _dbContext.Matches.FirstOrDefaultAsync(x => x.TeamA == createMatchesStatisticDto.ClubID 
                                                                            || x.TeamB == createMatchesStatisticDto.ClubID);
                if (club == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Club not found in Matches",
                        Data = null
                    };
                }
                if (player == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player not found in LineUP",
                        Data = null
                    };
                }
                if (LineUp == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "LineUp not found in club",
                        Data = null
                    };
                }
                if (createMatchesStatisticDto.Goal > createMatchesStatisticDto.Shot)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Goal must be less than Shot",
                        Data = null
                    };
                }
                if (createMatchesStatisticDto.Fouls < createMatchesStatisticDto.YellowCard + createMatchesStatisticDto.RedCard)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Fouls must be more  than YellowCard + RedCard",
                        Data = null
                    };
                }

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
                var LineUp = await _dbContext.LineUps.FirstOrDefaultAsync(x => x.LineUpID == updateMatchesStatisticDto.LineUpID
                                                                          && x.ClubID == matchesStatistic.ClubID);
                var player = await _dbContext.PlayerLineUps.FirstOrDefaultAsync(x => x.PlayerID == updateMatchesStatisticDto.PlayerID
                                                                            && x.LineUpID == updateMatchesStatisticDto.LineUpID);
                var club = await _dbContext.Matches.FirstOrDefaultAsync(x => x.TeamA == updateMatchesStatisticDto.ClubID
                                                                            || x.TeamB == updateMatchesStatisticDto.ClubID);
                if (club == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Club not found in Matches",
                        Data = null
                    };
                }
                if (player == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player not found in LineUP",
                        Data = null
                    };
                }
                if (LineUp == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "LineUp not found in club",
                        Data = null
                    };
                }
                if (updateMatchesStatisticDto.Goal > updateMatchesStatisticDto.Shot)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Goal must be less than Shot",
                        Data = null
                    };
                }
                if (updateMatchesStatisticDto.Fouls < updateMatchesStatisticDto.YellowCard + updateMatchesStatisticDto.RedCard)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Fouls must be more  than YellowCard + RedCard",
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
                matchesStatistic.half = updateMatchesStatisticDto.half;
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