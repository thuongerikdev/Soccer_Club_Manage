
using Microsoft.Extensions.Logging;
using SM.LineUp.ApplicationService.Common;
using SM.LineUp.ApplicationService.Module.Abtracts;
using SM.LineUp.Domain.LineUp;
using SM.LineUp.Dtos;
using SM.LineUp.Dtos.PlayerLineUp;
using SM.LineUp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.LineUp.ApplicationService.Module.Implements
{
    public class PlayerLineUPService : LineUpServiceBase , IPlayerLineUp
    {
        public PlayerLineUPService(ILogger<LineUpService> logger, LineUpDbContext dbContext) : base(logger, dbContext)
        {

        }
        public async Task<LineUpResponeDto> CreatePlayerLineUps(List<PlayerLineUpDto> playerLineUpDtos)
        {
            var response = new LineUpResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Create PlayerLineUps success",
                Data = null
            };

            try
            {
                foreach (var playerLineUpDto in playerLineUpDtos)
                {
                    var playerLineUp = new PlayerLineUp
                    {
                        LineUpId = playerLineUpDto.LineUpId,
                        PlayerId = playerLineUpDto.PlayerId,
                        PlayerPosition = playerLineUpDto.PlayerPosition,
                        ClubId = playerLineUpDto.ClubId,
                        IsCaptain = playerLineUpDto.IsCaptain,
                        PlayTime = playerLineUpDto.PlayTime
                    };

                    _dbContext.PlayerLineUps.Add(playerLineUp);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.ErrorCode = 1;
                response.ErrorMessage = "Create PlayerLineUps fail: " + ex.Message;
            }

            return response;
        }
        public async Task<LineUpResponeDto> UpdatePlayerLineUp(UpdatePlayerLineUpDto updatePlayerLineUpDto)
        {
            try
            {
                var existPlayerLineUp = await _dbContext.PlayerLineUps.FindAsync(updatePlayerLineUpDto.PlayerLineUpId);
                if (existPlayerLineUp == null)
                {
                    return new LineUpResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "PlayerLineUp not found",
                        Data = null
                    };
                }
                existPlayerLineUp.PlayerPosition = updatePlayerLineUpDto.PlayerPosition;
                existPlayerLineUp.IsCaptain = updatePlayerLineUpDto.IsCaptain;
                existPlayerLineUp.PlayTime = updatePlayerLineUpDto.PlayTime;
                existPlayerLineUp.ClubId = updatePlayerLineUpDto.ClubId;
                existPlayerLineUp.LineUpId = updatePlayerLineUpDto.LineUpId;
                existPlayerLineUp.PlayerId = updatePlayerLineUpDto.PlayerId;

                await _dbContext.SaveChangesAsync();
                return new LineUpResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update PlayerLineUp success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new LineUpResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Update PlayerLineUp fail",
                    Data = null
                };
            }
        }
        public async Task<LineUpResponeDto> RemovePlayerLineUp(int playerLineUpId)
        {
            try
            {
                var playerLineUp = await _dbContext.PlayerLineUps.FindAsync(playerLineUpId);
                if (playerLineUp == null)
                {
                    return new LineUpResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "PlayerLineUp not found",
                        Data = null
                    };
                }
                _dbContext.PlayerLineUps.Remove(playerLineUp);
                await _dbContext.SaveChangesAsync();
                return new LineUpResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Remove PlayerLineUp success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new LineUpResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Remove PlayerLineUp fail",
                    Data = null
                };
            }

        }
        public async ValueTask<LineUpResponeDto> GetAllPlayerLineUp()
        {
            try
            {
                var playerLineUp = _dbContext.PlayerLineUps.ToList();
                var playerLineUpDto = playerLineUp.Select(x => new PlayerLineUpDto
                {
                    LineUpId = x.LineUpId,
                    PlayerId = x.PlayerId,
                    ClubId = x.ClubId,
                    PlayerPosition = x.PlayerPosition,
                    IsCaptain = x.IsCaptain,
                    PlayTime = x.PlayTime
                });
                return new LineUpResponeDto
                {
                    Data= playerLineUpDto,
                    ErrorCode = 0,
                    ErrorMessage = "Get All PlayerLineUp success"
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async ValueTask<LineUpResponeDto> GetPlayerLineUpById(int playerLineUpId)
        {
            try
            {
                var playerLineUp = await _dbContext.PlayerLineUps.FindAsync(playerLineUpId);
                if (playerLineUp == null)
                {
                    return null;
                }
                var playerLineUpDto = new PlayerLineUpDto
                {
                    LineUpId = playerLineUp.LineUpId,
                    PlayerId = playerLineUp.PlayerId,
                    ClubId = playerLineUp.ClubId,
                    PlayerPosition = playerLineUp.PlayerPosition,
                    IsCaptain = playerLineUp.IsCaptain,
                    PlayTime = playerLineUp.PlayTime
                };
                return new LineUpResponeDto
                {
                    Data = playerLineUpDto,
                    ErrorCode = 0,
                    ErrorMessage = "Get PlayerLineUp success"
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
