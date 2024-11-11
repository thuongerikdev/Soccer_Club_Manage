using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.TournamentModule.PlayerModule.Abtracts;
using SM.Tournament.Domain.Player;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using SM.Tournament.Dtos.PlayerDto.PlayerFund;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TournamentModule.PlayerModule.Implements
{
    public  class PlayerFundService : TournamentServiceBase , IPlayerFundService
    {
        public PlayerFundService(ILogger <PlayerFundService>logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreatePlayerFund(CreatePlayerFundDto createPlayerFundDto)
        {
            try
            {
                var playerFund = new PlayerFund
                {
                    ClubID = createPlayerFundDto.ClubID,
                    PlayerID = createPlayerFundDto.PlayerID,
                    Amount = createPlayerFundDto.Amount,
                    Status = createPlayerFundDto.Status,
                    FundActionHistoryID = createPlayerFundDto.FundActionHistoryID

                };
                _dbContext.PlayerFunds.Add(playerFund);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Player Fund Created Successfully",
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
        public async Task<TournamentResponeDto> UpdatePlayerFund(UpdatePlayerFundDto updatePlayerFundDto)
        {
            try
            {
                var playerFund = _dbContext.PlayerFunds.Find(updatePlayerFundDto.PlayerFundID);
                if (playerFund == null) throw new ArgumentException("Player Fund not found.");
                playerFund.Amount = updatePlayerFundDto.Amount;
                playerFund.Status = updatePlayerFundDto.Status;
                playerFund.FundActionHistoryID = updatePlayerFundDto.FundActionHistoryID;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Player Fund Updated Successfully",
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
        public async Task<TournamentResponeDto> DeletePlayerFund(int playerFundID)
        {
            try
            {
                var playerFund = _dbContext.PlayerFunds.Find(playerFundID);
                if (playerFund == null) throw new ArgumentException("Player Fund not found.");
                _dbContext.PlayerFunds.Remove(playerFund);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Player Fund Deleted Successfully",
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
        public async Task<TournamentResponeDto> GetPlayerFundById(int playerFundID)
        {
            try
            {
                var playerFund = _dbContext.PlayerFunds.Find(playerFundID);
                if (playerFund == null) throw new ArgumentException("Player Fund not found.");
                return new TournamentResponeDto
                {
                    ErrorMessage = "Player Fund Found",
                    ErrorCode = 0,
                    Data = playerFund
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
        public async Task<TournamentResponeDto> GetAllPlayerFund()
        {
            try
            {
                var playerFunds = _dbContext.PlayerFunds.ToList();
                return new TournamentResponeDto
                {
                    ErrorMessage = "Player Funds Found",
                    ErrorCode = 0,
                    Data = playerFunds
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

    }
}
