using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.PlayerModule.Abtracts;
using SM.Tournament.Domain.Player;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.Player;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.PlayerModule.Implements
{
    public class PlayerService : TournamentServiceBase, IPlayerService
    {
        public PlayerService(ILogger<PlayerService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            try
            {

                var player = new ClubPlayers
                {
                    PhoneNumber = createPlayerDto.PhoneNumber,
                    PlayerName = createPlayerDto.PlayerName,
                    PlayerAge = createPlayerDto.PlayerAge,
                    PlayerPosition = createPlayerDto.PlayerPosition,
                    PlayerImage = createPlayerDto.PlayerImage,
                    PlayerStatus = createPlayerDto.PlayerStatus,
                    ClubID = createPlayerDto.ClubID,
                    height = createPlayerDto.height,
                    leg = createPlayerDto.leg,
                    weight = createPlayerDto.weight,
                    Shirtnumber = createPlayerDto.Shirtnumber,
                };
                var playerNumber = await _dbContext.ClubPlayers.FirstOrDefaultAsync ( x => x.Shirtnumber == createPlayerDto.Shirtnumber);
                if ( playerNumber != null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Shirt Number is already exist",
                        Data = null
                    };
                }
                _dbContext.ClubPlayers.Add(player);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Player Success",
                    Data = player.PlayerID
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
        public async Task<TournamentResponeDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            try
            {
                var player = _dbContext.ClubPlayers.Find(updatePlayerDto.PlayerID);
                if (player == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player not found",
                        Data = null
                    };
                }
                player.PlayerName = updatePlayerDto.PlayerName;
                player.PlayerAge = updatePlayerDto.PlayerAge;
                player.PlayerPosition = updatePlayerDto.PlayerPosition;
                player.PlayerImage = updatePlayerDto.PlayerImage;
                player.PlayerStatus = updatePlayerDto.PlayerStatus;
                player.height = updatePlayerDto.height;
                player.leg = updatePlayerDto.leg;
                player.weight = updatePlayerDto.weight;
                player.Shirtnumber = updatePlayerDto.Shirtnumber;
                _dbContext.ClubPlayers.Update(player);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Player Success",
                    Data = player.PlayerID
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
        public async Task<TournamentResponeDto> DeletePlayer(int playerID)
        {
            try
            {
                var player = _dbContext.ClubPlayers.Find(playerID);
                if (player == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player not found",
                        Data = null
                    };
                }
                _dbContext.ClubPlayers.Remove(player);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Player Success",
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
        public async Task<TournamentResponeDto> GetPlayerById(int playerID)
        {
            try
            {
                var player = _dbContext.ClubPlayers.Find(playerID);
                if (player == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Player not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Player Success",
                    Data = player
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
        public async Task<TournamentResponeDto> GetPlayersByClubId(int clubID)
        {
            try
            {
                var players = _dbContext.ClubPlayers.Where(x => x.ClubID == clubID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Players Success",
                    Data = players
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
        public async Task<TournamentResponeDto> GetAllPlayer()
        {
            try
            {
                var players = _dbContext.ClubPlayers.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Players Success",
                    Data = players
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
        public  async Task<TournamentResponeDto> GetPlayerByCLub(int clubId)
        {
            var player =   _dbContext.ClubPlayers.Where(x => x.ClubID == clubId).ToList();
            if (player == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Player not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Player Success",
                Data = player
            };

        }
    }
}