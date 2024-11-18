using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.LineUpModule.Abtracts;
using SM.Tournament.Domain.LineUp;
using SM.Tournament.Domain.Player;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.LineUpDto.LineUp;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.LineUpModule.Implements
{
    public class LineUpService : TournamentServiceBase, ILineUpService
    {
        public LineUpService(ILogger<LineUpService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreateLineUp(CreateLineUpDto createLineUpDto)
        {
            try
            {
                var lineUp = new LineUpBase
                {
                    ClubID = createLineUpDto.ClubID,
                    LineUpName = createLineUpDto.LineUpName,
                    LineUpType = createLineUpDto.LineUpType,
                    PlayerNumber = createLineUpDto.PlayerNumber,
                    CreateAt = DateTime.Now,

                };
                _dbContext.LineUps.Add(lineUp);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Line Up Created Successfully",
                    ErrorCode = 0,
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
        public async Task<TournamentResponeDto> UpdateLineUp(UpdateLineUpDto updateLineUpDto)
        {
            try
            {
                var lineUp = _dbContext.LineUps.Find(updateLineUpDto.LineUpID);
                if (lineUp == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorMessage = "Line Up not found",
                        ErrorCode = 1,
                        Data = null
                    };
                }
                lineUp.LineUpName = updateLineUpDto.LineUpName;
                lineUp.LineUpType = updateLineUpDto.LineUpType;
                lineUp.PlayerNumber = updateLineUpDto.PlayerNumber;
                lineUp.CreateAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Line Up Updated Successfully",
                    ErrorCode = 0,
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
        public async Task<TournamentResponeDto> DeleteLineUp(int lineUpID)
        {
            try

            {
                var playerLineup = _dbContext.PlayerLineUps.Where(x => x.LineUpID == lineUpID).ToList();
               

                foreach (PlayerLineUp player in playerLineup)
                {
                     _dbContext.PlayerLineUps.Remove(player);
                }

                var lineUp = _dbContext.LineUps.FirstOrDefault(x => x.LineUpID == lineUpID);
                if (lineUp == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Line Up not found",
                        Data = null
                    };
                }
                _dbContext.LineUps.Remove(lineUp);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Line Up Success",
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
        public async Task<TournamentResponeDto> GetLineUpById(int lineUpID)
        {
            try
            {
                var lineUp = _dbContext.LineUps.FirstOrDefault(x => x.LineUpID == lineUpID);
                if (lineUp == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Line Up not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Line Up Found",
                    Data = lineUp
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
        public async Task<TournamentResponeDto> GetAllLineUp()
        {
            try
            {
                var lineUps = _dbContext.LineUps.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Line Up List",
                    Data = lineUps
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
        public async Task<TournamentResponeDto> GetLineUpbyClub(int clubID)
        {
            try
            {
                var lineUps = _dbContext.LineUps.Where(x => x.ClubID == clubID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Line Up List",
                    Data = lineUps
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
