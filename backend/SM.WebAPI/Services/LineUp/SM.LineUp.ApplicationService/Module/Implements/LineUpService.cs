using Microsoft.Extensions.Logging;
using SM.LineUp.ApplicationService.Common;
using SM.LineUp.ApplicationService.Module.Abtracts;
using SM.LineUp.Domain.LineUp;
using SM.LineUp.Infrastructure;
using SM.LineUp.Dtos;
using SM.LineUp.Dtos.LineUpDtos;

namespace SM.LineUp.ApplicationService.Module.Implements
{
    public class LineUpService : LineUpServiceBase, ILineUpService
    {
        public LineUpService(ILogger<LineUpService> logger, LineUpDbContext dbContext) : base(logger, dbContext)
        {

        }
        public async Task<LineUpResponeDto> CreateLineUp(CreateLineUpDto createLineUpDto)
        {
            try
            {
                var lineUp = new LineUpBase
                {
                    ClubId = createLineUpDto.ClubId,
                    LineUpName = createLineUpDto.LineUpName,
                    LineUpType = createLineUpDto.LineUpType,
                    MatchType = createLineUpDto.MatchType,
                    MatchId = createLineUpDto.MatchId,
                    CreateAt = DateTime.UtcNow, // Automatically set to current date and time
                    StadiumBackGroud = createLineUpDto.StadiumBackGroud,
                };

                _dbContext.LineUps.Add(lineUp);
                await _dbContext.SaveChangesAsync();

                return new LineUpResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create LineUp success",
                    Data = null
                };
            }
            catch
            {
                return new LineUpResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Create LineUp fail",
                    Data = null
                };
            }
        }
        public async Task<LineUpResponeDto> UpdateLineUp(UpdateLineUpDto updateLineUpDto)
        {
            try
            {
                var existLineUp = await _dbContext.LineUps.FindAsync(updateLineUpDto.LineUpId);
                if (existLineUp == null)
                {
                    return new LineUpResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "LineUp not found",
                        Data = null
                    };
                }
                existLineUp.LineUpName = updateLineUpDto.LineUpName;
                existLineUp.LineUpType = updateLineUpDto.LineUpType;
                existLineUp.MatchType = updateLineUpDto.MatchType;
                existLineUp.StadiumBackGroud = updateLineUpDto.StadiumBackGroud;
                existLineUp.MatchId = updateLineUpDto.MatchId;
                existLineUp.ClubId = updateLineUpDto.ClubId;


                await _dbContext.SaveChangesAsync();
                return new LineUpResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update LineUp success",
                    Data = null
                };
            }
            catch
            {
                return new LineUpResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Update LineUp fail",
                    Data = null
                };
            }
        }
        public async Task<LineUpResponeDto> RemoveLineUp(int lineUpId)
        {
            try
            {
                var lineUp = await _dbContext.LineUps.FindAsync(lineUpId);
                if (lineUp == null)
                {
                    return new LineUpResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "LineUp not found",
                        Data = null
                    };
                }
                _dbContext.LineUps.Remove(lineUp);
                await _dbContext.SaveChangesAsync();
                return new LineUpResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete LineUp success",
                    Data = null
                };
            }
            catch
            {
                return new LineUpResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "RErrorMessageove LineUp fail",
                    Data = null
                };
            }
        }
        public async ValueTask<LineUpResponeDto> GetAllLineUp()
        {
            try
            {
                var lineUps = _dbContext.LineUps.ToList();
                var readLineUpDto = new List<ReadLineUpDto>();
                foreach (var lineUp in lineUps)
                {
                    readLineUpDto.Add(new ReadLineUpDto
                    {
                        LineUpId = lineUp.LineUpId,
                        LineUpName = lineUp.LineUpName,
                        LineUpType = lineUp.LineUpType,
                        MatchType = lineUp.MatchType,
                        CreateAt = lineUp.CreateAt,
                        StadiumBackGroud = lineUp.StadiumBackGroud,
                        MatchId = lineUp.MatchId,
                        ClubId = lineUp.ClubId

                    });
                }
                return new LineUpResponeDto
                {
                    ErrorCode = 0 ,
                    ErrorMessage = "Get all LineUp success",
                    Data = readLineUpDto
                };
            }
            catch
            {
                return new LineUpResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Get all LineUp fail",
                    Data = null
                };
            }
        }
        public async ValueTask<LineUpResponeDto> GetLineUpById(int lineUpId)
        {
            try
            {
                var lineUp = await _dbContext.LineUps.FindAsync(lineUpId);
                if (lineUp == null)
                {
                    return new LineUpResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "LineUp not found",
                        Data = null
                    };
                }
                return new LineUpResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get LineUp success",
                    Data = new ReadLineUpDto
                    {
                        LineUpId = lineUp.LineUpId,
                        LineUpName = lineUp.LineUpName,
                        LineUpType = lineUp.LineUpType,
                        MatchType = lineUp.MatchType,
                        CreateAt = lineUp.CreateAt,
                        StadiumBackGroud = lineUp.StadiumBackGroud,
                        MatchId = lineUp.MatchId,
                        ClubId = lineUp.ClubId

                    }
                };
            }
            catch
            {
                return new LineUpResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Get LineUp fail",
                    Data = null
                };
            }
        }

    }
}
