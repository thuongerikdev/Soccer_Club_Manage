using Microsoft.Extensions.Logging;
using SM.Match.ApplicationService.Common;
using SM.Match.ApplicationService.Module.MatchesModule.Abtracts;
using SM.Match.Domain;
using SM.Match.Domain.Statistic;
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
                    PlayerId = createMatchStatisticDto.PlayerId,
                    Score = createMatchStatisticDto.Score,
                    Shot = createMatchStatisticDto.Shot,
                    Pass = createMatchStatisticDto.Pass,
                    Fouls = createMatchStatisticDto.Fouls,
                    LineUpId = createMatchStatisticDto.LineUpId,



                };
                 _dbContext.MatchesStatistics.Add(matchesStatistic);
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Success",
                    Data = ""
                };
            }
            catch (Exception e) {           
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = e.Message,
                    Data = ""
                };
            }
        }
        public  async Task<MatchResponeDto> UpdateMatchStatistic(UpdateMatchesStatisticDto updateMatchStatisticDto)
        {
            try
            {
                var matchStatistic = await _dbContext.MatchesStatistics.FindAsync(updateMatchStatisticDto.MatchesStatisticId);
                if (matchStatistic == null)
                {
                    return new MatchResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match Statistic not found",
                        Data = ""
                    };

                }
                matchStatistic.MatchesId = updateMatchStatisticDto.MatchesId;
                matchStatistic.ClubId = updateMatchStatisticDto.ClubId;
                matchStatistic.PlayerId = updateMatchStatisticDto.PlayerId;
                matchStatistic.Fouls = updateMatchStatisticDto.Fouls;
                matchStatistic.Pass = updateMatchStatisticDto.Pass;
                matchStatistic.Score = updateMatchStatisticDto.Score;
                matchStatistic.Shot = updateMatchStatisticDto.Shot;
                matchStatistic.LineUpId = updateMatchStatisticDto.LineUpId;
                await _dbContext.SaveChangesAsync();
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Match Statistic Success",
                    Data = ""
                };
            }

            catch (Exception e)
            {
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = e.Message,
                    Data = ""
                };
            }
        }
        public async Task<MatchResponeDto> RemoveMatchStatistic(int matchStatisticId)
        {
            try
            {
                var matchStatistic = await _dbContext.MatchesStatistics.FindAsync(matchStatisticId);
                if (matchStatistic == null)
                {
                    return new MatchResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match Statistic not found",
                        Data = ""
                    };
                }
                _dbContext.MatchesStatistics.Remove(matchStatistic);
                await _dbContext.SaveChangesAsync();
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Remove Match Statistic Success",
                    Data = ""
                };
            }
            catch (Exception e)
            {
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = e.Message,
                    Data = ""
                };

            }
        }
        public async  ValueTask<MatchResponeDto> GetAllMatchStatistic()
        {
            try
            {
                var matchStatistic = _dbContext.MatchesStatistics.ToList();
                var matchStatisticDto = matchStatistic.Select(x => new GetMatchesStatisticDto
                {
                    MatchesStatisticId = x.MatchesStatisticId,
                    MatchesId = x.MatchesId,
                    ClubId = x.ClubId,
                    PlayerId = x.PlayerId,
                    Score= x.Score,
                    Fouls= x.Fouls,
                    Shot= x.Shot,
                    Pass= x.Pass,
                    LineUpId= x.LineUpId,


                });
                return new MatchResponeDto
                {
                    ErrorCode = 0 ,
                    ErrorMessage = "Find sucess",
                    Data = matchStatisticDto
                };
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
                var matchStatistic = await _dbContext.MatchesStatistics.FindAsync(matchStatisticId);
                if (matchStatistic == null)
                {
                    return new MatchResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match Statistic not found",
                        Data = ""
                    };
                }
                var matchStatisticDto = new GetMatchesStatisticDto
                {
                    MatchesStatisticId = matchStatistic.MatchesStatisticId,
                    MatchesId = matchStatistic.MatchesId,
                    ClubId = matchStatistic.ClubId,
                    Pass = matchStatistic.Pass,
                    Shot = matchStatistic.Shot,
                    Score = matchStatistic.Score,
                    Fouls = matchStatistic.Fouls,
                    LineUpId= matchStatistic.LineUpId,
                    PlayerId= matchStatistic.PlayerId,

                };
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Find sucess",
                    Data = matchStatisticDto
                };
            }
            catch (Exception e)
            {
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = e.Message,
                    Data = ""
                };
            }
        }
    }
}
