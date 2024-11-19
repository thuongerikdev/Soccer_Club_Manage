using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Minigame;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements
{
    public class MinigameService : TournamentServiceBase , IMinigameService
    {
        public MinigameService(ILogger <MinigameService> logger , TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreateMinigame(CreateMinigameDto createMinigameDto)
        {
            try
            {
                var minigame = new Minigames
                {
                    TournamentID = createMinigameDto.TournamentID,
                    MinigameType = createMinigameDto.MinigameType,
                    StartDates = createMinigameDto.StartDates,
                    EndDates = createMinigameDto.EndDates,
                    Name = createMinigameDto.Name,
                    Description = createMinigameDto.Description
                };
                _dbContext.Minigames.Add(minigame);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Minigame Success",
                    Data = minigame.MinigameID
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
        public async Task<TournamentResponeDto> UpdateMinigame(UpdateMinigameDto updateMinigameDto)
        {
            try
            {
                var minigame = await _dbContext.Minigames.FindAsync(updateMinigameDto.MinigameID);
                if (minigame == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Minigame not found",
                        Data = null
                    };
                }
                minigame.TournamentID = updateMinigameDto.TournamentID;
                minigame.MinigameType = updateMinigameDto.MinigameType;
                minigame.StartDates = updateMinigameDto.StartDates;
                minigame.EndDates = updateMinigameDto.EndDates;
                minigame.Name = updateMinigameDto.Name;
                minigame.Description = updateMinigameDto.Description;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Minigame Success",
                    Data = minigame.MinigameID
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
        public async Task<TournamentResponeDto> DeleteMinigame(int MinigameID)
        {
            try
            {
                var minigame = await _dbContext.Minigames.FindAsync(MinigameID);
                if (minigame == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Minigame not found",
                        Data = null
                    };
                }
                _dbContext.Minigames.Remove(minigame);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Minigame Success",
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
        public async Task<TournamentResponeDto> GetMinigame(int MinigameID)
        {
            try
            {
                var minigame = _dbContext.Minigames.Find(MinigameID);
                if (minigame == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Minigame not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Minigame Success",
                    Data = minigame
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
        public async Task<TournamentResponeDto> GetMinigames()
        {
            try
            {
                var minigames = _dbContext.Minigames.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Minigames Success",
                    Data = minigames
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
        //public async Task<TournamentResponeDto> GetMinigamesByTournamentID(int TournamentID)
        //{
        //    try
        //    {
        //        var minigames = _dbContext.Minigames.Where(x => x.TournamentID == TournamentID).ToList();
        //        return new TournamentResponeDto
        //        {
        //            ErrorCode = 0,
        //            ErrorMessage = "Get Minigames Success",
        //            Data = minigames
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new TournamentResponeDto
        //        {
        //            ErrorCode = 1,
        //            ErrorMessage = ex.Message,
        //            Data = null
        //        };
        //    }
        //}
        //public async Task<TournamentResponeDto> GetMinigamesByMinigameType(string MinigameType)
        //{
        //    try
        //    {
        //        var minigames = _dbContext.Minigames.Where(x => x.MinigameType == MinigameType).ToList();
        //        return new TournamentResponeDto
        //        {
        //            ErrorCode = 0,
        //            ErrorMessage = "Get Minigames Success",
        //            Data = minigames
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new TournamentResponeDto
        //        {
        //            ErrorCode = 1,
        //            ErrorMessage = ex.Message,
        //            Data = null
        //        };
        //    }
        //}
        //public async Task<TournamentResponeDto> GetMinigamesByStartDates(DateTime StartDates)
        //{
        //    try
        //    {
        //        var minigames = _dbContext.Minigames.Where(x => x.StartDates == StartDates).ToList();
        //        return new TournamentResponeDto
        //        {
        //            ErrorCode = 0,
        //            ErrorMessage = "Get Minigames Success",
        //            Data = minigames
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new TournamentResponeDto
        //        {
        //            ErrorCode = 1,
        //            ErrorMessage = ex.Message,
        //            Data = null
        //        };
        //    }
        //}
    }
}
