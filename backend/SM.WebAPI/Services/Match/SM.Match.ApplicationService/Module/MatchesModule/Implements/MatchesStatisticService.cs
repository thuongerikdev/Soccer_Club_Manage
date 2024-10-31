using Microsoft.Extensions.Logging;
using SM.Match.ApplicationService.Common;
using SM.Match.ApplicationService.Module.MatchesModule.Abtracts;
using SM.Match.Domain.Matches;
using SM.Match.Dtos;
using SM.Match.Dtos.MatchesDto.MatchesStatistic;
using SM.Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SM.Tournament.ApplicationService.Module.MatchesModule.Implements
{
    public class MatchesStatisticService: MatchServiceBase , IMatchesStatisticService
    {
        public MatchesStatisticService(ILogger<MatchesStatisticService> logger, MatchDbContext dbContext) : base(logger, dbContext)
        {

        }
        public async Task<MatchResponeDto> CreateMatchStatistic(CreateMatchesStatisticDto createMatchStatisticDto)
        {
            try
            {
                var matchesStatistic = new MatchesStatistic
                {
                    MatchesId = createMatchStatisticDto.MatchesId,
                    ClubId = createMatchStatisticDto.ClubId,
                    goal   = createMatchStatisticDto.goal,
                    yellowCard = createMatchStatisticDto.yellowCard,
                    redCard = createMatchStatisticDto.redCard,
                    assist = createMatchStatisticDto.assist,
                    penalty = createMatchStatisticDto.penalty,
                    goalAgainst = createMatchStatisticDto.goalAgainst,
                    cleanSheet = createMatchStatisticDto.cleanSheet,
                    ownGoal = createMatchStatisticDto.ownGoal,
                    penaltySaved = createMatchStatisticDto.penaltySaved,
                    penaltyMissed = createMatchStatisticDto.penaltyMissed,
                    matchPlayed = createMatchStatisticDto.matchPlayed,
                    Corner = createMatchStatisticDto.Corner,
                    Offside = createMatchStatisticDto.Offside,
                    Fouls = createMatchStatisticDto.Fouls,
                    Shots = createMatchStatisticDto.Shots,
                    ShotsOnTarget = createMatchStatisticDto.ShotsOnTarget,
                    ShotsOffTarget = createMatchStatisticDto.ShotsOffTarget,
                    Possession = createMatchStatisticDto.Possession,
                    Passes = createMatchStatisticDto.Passes,
                    PassesCompleted = createMatchStatisticDto.PassesCompleted,
                    Tackles = createMatchStatisticDto.Tackles


                };
                 _dbContext.MatchesStatistic.Add(matchesStatistic);
                return new MatchResponeDto
                {
                    EC = 0,
                    EM = "Success",
                    DT = ""
                };
            }
            catch (Exception e) {           
                return new MatchResponeDto
                {
                    EC = -1,
                    EM = e.Message,
                    DT = ""
                };
            }
        }
        public  async Task<MatchResponeDto> UpdateMatchStatistic(UpdateMatchesStatisticDto updateMatchStatisticDto)
        {
            try
            {
                var matchStatistic = await _dbContext.MatchesStatistic.FindAsync(updateMatchStatisticDto.MatchesStatisticId);
                if (matchStatistic == null)
                {
                    return new MatchResponeDto
                    {
                        EC = 1,
                        EM = "Match Statistic not found",
                        DT = ""
                    };

                }
                matchStatistic.MatchesId = updateMatchStatisticDto.MatchesId;
                matchStatistic.ClubId = updateMatchStatisticDto.ClubId;
                matchStatistic.goal= updateMatchStatisticDto.goal;
                matchStatistic.yellowCard = updateMatchStatisticDto.yellowCard;
                matchStatistic.redCard = updateMatchStatisticDto.redCard;
                matchStatistic.assist = updateMatchStatisticDto.assist;
                matchStatistic.penalty = updateMatchStatisticDto.penalty;
                matchStatistic.goalAgainst = updateMatchStatisticDto.goalAgainst;
                matchStatistic.cleanSheet = updateMatchStatisticDto.cleanSheet;
                matchStatistic.ownGoal = updateMatchStatisticDto.ownGoal;
                matchStatistic.penaltySaved = updateMatchStatisticDto.penaltySaved;
                matchStatistic.penaltyMissed = updateMatchStatisticDto.penaltyMissed;
                matchStatistic.matchPlayed = updateMatchStatisticDto.matchPlayed;
                matchStatistic.Corner = updateMatchStatisticDto.Corner;
                matchStatistic.Offside = updateMatchStatisticDto.Offside;
                matchStatistic.Fouls = updateMatchStatisticDto.Fouls;
                matchStatistic.Shots = updateMatchStatisticDto.Shots;
                matchStatistic.ShotsOnTarget = updateMatchStatisticDto.ShotsOnTarget;
                matchStatistic.ShotsOffTarget = updateMatchStatisticDto.ShotsOffTarget;
                matchStatistic.Passes = updateMatchStatisticDto.Passes;
                matchStatistic.PassesCompleted = updateMatchStatisticDto.PassesCompleted;
                matchStatistic.Tackles = updateMatchStatisticDto.Tackles;

                await _dbContext.SaveChangesAsync();
                return new MatchResponeDto
                {
                    EC = 0,
                    EM = "Update Match Statistic Success",
                    DT = ""
                };
            }

            catch (Exception e)
            {
                return new MatchResponeDto
                {
                    EC = -1,
                    EM = e.Message,
                    DT = ""
                };
            }
        }
        public async Task<MatchResponeDto> RemoveMatchStatistic(int matchStatisticId)
        {
            try
            {
                var matchStatistic = await _dbContext.MatchesStatistic.FindAsync(matchStatisticId);
                if (matchStatistic == null)
                {
                    return new MatchResponeDto
                    {
                        EC = 1,
                        EM = "Match Statistic not found",
                        DT = ""
                    };
                }
                _dbContext.MatchesStatistic.Remove(matchStatistic);
                await _dbContext.SaveChangesAsync();
                return new MatchResponeDto
                {
                    EC = 0,
                    EM = "Remove Match Statistic Success",
                    DT = ""
                };
            }
            catch (Exception e)
            {
                return new MatchResponeDto
                {
                    EC = -1,
                    EM = e.Message,
                    DT = ""
                };

            }
        }
        public async  ValueTask<IEnumerable<GetMatchesStatisticDto>> GetAllMatchStatistic()
        {
            try
            {
                var matchStatistic = _dbContext.MatchesStatistic.ToList();
                var matchStatisticDto = matchStatistic.Select(x => new GetMatchesStatisticDto
                {
                    MatchesStatisticId = x.MatchesStatisticId,
                    MatchesId = x.MatchesId,
                    ClubId = x.ClubId,
                    goal   = x.goal,
                    yellowCard = x.yellowCard,
                    redCard = x.redCard,
                    assist = x.assist,
                    penalty = x.penalty,
                    goalAgainst = x.goalAgainst,
                    cleanSheet = x.cleanSheet,
                    ownGoal = x.ownGoal,
                    penaltySaved = x.penaltySaved,
                    penaltyMissed = x.penaltyMissed,
                    matchPlayed = x.matchPlayed,
                    Corner = x.Corner,
                    Offside = x.Offside,
                    Fouls = x.Fouls,
                    Shots = x.Shots,
                    ShotsOnTarget = x.ShotsOnTarget,
                    ShotsOffTarget = x.ShotsOffTarget,
                    Possession = x.Possession,
                    Passes = x.Passes,
                    PassesCompleted = x.PassesCompleted,
                    Tackles = x.Tackles

                });
                return matchStatisticDto;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async ValueTask<MatchResponeDto> GetMatchStatisticById(int matchStatisticId)
        {
            try
            {
                var matchStatistic = await _dbContext.MatchesStatistic.FindAsync(matchStatisticId);
                if (matchStatistic == null)
                {
                    return new MatchResponeDto
                    {
                        EC = 1,
                        EM = "Match Statistic not found",
                        DT = ""
                    };
                }
                var matchStatisticDto = new GetMatchesStatisticDto
                {
                    MatchesStatisticId = matchStatistic.MatchesStatisticId,
                    MatchesId = matchStatistic.MatchesId,
                    ClubId = matchStatistic.ClubId,
                    goal = matchStatistic.goal,
                    yellowCard = matchStatistic.yellowCard,
                    redCard = matchStatistic.redCard,
                    assist = matchStatistic.assist,
                    penalty = matchStatistic.penalty,
                    goalAgainst = matchStatistic.goalAgainst,
                    cleanSheet = matchStatistic.cleanSheet,
                    ownGoal = matchStatistic.ownGoal,
                    penaltySaved = matchStatistic.penaltySaved,
                    penaltyMissed = matchStatistic.penaltyMissed,
                    matchPlayed = matchStatistic.matchPlayed,
                    Corner = matchStatistic.Corner,
                    Offside = matchStatistic.Offside,
                    Fouls = matchStatistic.Fouls,
                    Shots = matchStatistic.Shots,
                    ShotsOnTarget = matchStatistic.ShotsOnTarget,
                    ShotsOffTarget = matchStatistic.ShotsOffTarget,
                    Possession = matchStatistic.Possession,
                    Passes = matchStatistic.Passes,
                    PassesCompleted = matchStatistic.PassesCompleted,
                    Tackles = matchStatistic.Tackles

                };
                return new MatchResponeDto
                {
                    EC = 0,
                    EM = "Find sucess",
                    DT = matchStatisticDto
                };
            }
            catch (Exception e)
            {
                return new MatchResponeDto
                {
                    EC = -1,
                    EM = e.Message,
                    DT = ""
                };
            }
        }
    }
}
