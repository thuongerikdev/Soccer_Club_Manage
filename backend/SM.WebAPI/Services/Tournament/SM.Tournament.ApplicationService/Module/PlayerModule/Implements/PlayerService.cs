using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Domain.Club;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto;
using SM.Tournament.Domain.Players;
using SM.Tournament.ApplicationService.Module.PlayerModule.Abtracts;
using SM.Tournament.ApplicationService.Module.ClubModule.Implements;

namespace SM.Tournament.ApplicationService.Module.PlayerModule.Implements
{
    public class PlayerService : ClubServiceBase, IPlayerService
    {
        public PlayerService(ILogger<ClubTeamService> logger, TournamentDBContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            try
            {
                var player = new ClubPlayers
                {
                    PlayerName = createPlayerDto.PlayerName,
                    PlayerPosition = createPlayerDto.PlayerPosition,
                    PlayerNationality = createPlayerDto.PlayerNationality,
                    PlayerImage = createPlayerDto.PlayerImage,
                    PlayerAge = createPlayerDto.PlayerAge,
                    PlayerValue = createPlayerDto.PlayerValue,
                    PlayerHealth = createPlayerDto.PlayerHealth,
                    PlayerSkill = createPlayerDto.PlayerSkill,
                    PlayerSalary = createPlayerDto.PlayerSalary,
                };
                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    EC = 0,
                    EM = "create Player Success",
                    DT = ""
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = ""
                };
            }

        }
        public async Task<TournamentResponeDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            try
            {
                var existPlayer = _dbContext.Players.FindAsync(updatePlayerDto.PlayerId);
                if (existPlayer == null)
                {
                    return new TournamentResponeDto
                    {
                        EC = 1,
                        EM = "Not found Player",
                        DT = ""
                    };
                }
                var player = new ClubPlayers
                {
                    PlayerName = updatePlayerDto.PlayerName,
                    PlayerPosition = updatePlayerDto.PlayerPosition,
                    PlayerNationality = updatePlayerDto.PlayerNationality,
                    PlayerImage = updatePlayerDto.PlayerImage,
                    PlayerAge = updatePlayerDto.PlayerAge,
                    PlayerValue = updatePlayerDto.PlayerValue,
                    PlayerHealth = updatePlayerDto.PlayerHealth,
                    PlayerSkill = updatePlayerDto.PlayerSkill,
                    PlayerSalary = updatePlayerDto.PlayerSalary,
                    ClubId = updatePlayerDto.ClubId,
                };
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    EC = 0,
                    EM = "Update Player Success",
                    DT = null
                };

            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = null
                };
            }
        }
        public async Task<TournamentResponeDto> DeletePlayer(int playerId)
        {
            try
            {
                // Đợi để lấy player
                var player = await _dbContext.Players.FindAsync(playerId);
                if (player == null)
                {
                    return new TournamentResponeDto
                    {
                        EC = 1,
                        EM = "Not found Player",
                        DT = ""
                    };
                }

                // Xóa player
                _dbContext.Players.Remove(player);

                // Lưu thay đổi
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    EC = 0,
                    EM = "Delete Player Success",
                    DT = ""
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = null
                };
            }
        }
        public async ValueTask<IEnumerable<ClubPlayers>> GetAllPlayer()
        {
            try
            {
                var players = _dbContext.Players.ToList();

                var PlayerList = players.Select(x => new ClubPlayers
                {
                    PlayerId = x.PlayerId,
                    PlayerAge = x.PlayerAge,
                    PlayerName = x.PlayerName,
                    PlayerHealth = x.PlayerHealth,
                    PlayerImage = x.PlayerImage,
                    PlayerNationality = x.PlayerNationality,
                    PlayerPosition = x.PlayerPosition,
                    PlayerSalary = x.PlayerSalary,
                    PlayerSkill = x.PlayerSkill,
                    PlayerValue = x.PlayerValue,
                    ClubId = x.ClubId,
                }).ToList();
                return PlayerList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async ValueTask<TournamentResponeDto> GetPlayerById(int PlayerId)
        {
            try
            {
                var player = _dbContext.Players.FindAsync(PlayerId);
                if (player == null)
                {
                    return new TournamentResponeDto
                    {
                        EC = 1,
                        EM = "Not found Player",
                        DT = null
                    };
                }
                return new TournamentResponeDto
                {
                    EC = -1,
                    EM = "find Player Sucess",
                    DT = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = null
                };
            }
        }




    }
}
