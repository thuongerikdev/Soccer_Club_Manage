using Microsoft.Extensions.Logging;
using SM.Player.ApplicationService.Common;
using SM.Player.ApplicationService.Module.PlayerModule.Abtracts;
using SM.Player.Domain.Players;
using SM.Player.Dtos;
using SM.Player.Dtos.PlayerDto;
using SM.Player.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SM.Player.ApplicationService.Module.PlayerModule.Implements
{
    public class PlayerService : PlayerServiceBase, IPlayerService
    {
        public PlayerService(ILogger<PlayerService> logger, PlayerDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<PlayerResponeDto> CreatePlayer(CreatePlayerDto createPlayerDto)
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
                    PlayerStatus = createPlayerDto.PlayerStatus,
                    Shirtnumber = createPlayerDto.Shirtnumber,
                    ClubId = createPlayerDto.ClubId,
                    height = createPlayerDto.height,
                    weight = createPlayerDto.weight,
                    leg = createPlayerDto.leg,

                };
                _dbContext.Players.Add(player);
                await _dbContext.SaveChangesAsync();
                return new PlayerResponeDto
                {
                    EC = 0,
                    EM = "create Player Success",
                    DT = ""
                };
            }
            catch (Exception ex)
            {
                return new PlayerResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = ""
                };
            }

        }
        public async Task<PlayerResponeDto> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            try
            {
                var existPlayer = _dbContext.Players.FindAsync(updatePlayerDto.PlayerId);
                if (existPlayer == null)
                {
                    return new PlayerResponeDto
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
                    PlayerStatus = updatePlayerDto.PlayerStatus,
                    Shirtnumber = updatePlayerDto.Shirtnumber,
                    height = updatePlayerDto.height,
                    weight = updatePlayerDto.weight,
                    leg = updatePlayerDto.leg,
                };
                await _dbContext.SaveChangesAsync();
                return new PlayerResponeDto
                {
                    EC = 0,
                    EM = "Update Player Success",
                    DT = null
                };

            }
            catch (Exception ex)
            {
                return new PlayerResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = null
                };
            }
        }
        public async Task<PlayerResponeDto> DeletePlayer(int playerId)
        {
            try
            {
                // Đợi để lấy player
                var player = await _dbContext.Players.FindAsync(playerId);
                if (player == null)
                {
                    return new PlayerResponeDto
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
                return new PlayerResponeDto
                {
                    EC = 0,
                    EM = "Delete Player Success",
                    DT = ""
                };
            }
            catch (Exception ex)
            {
                return new PlayerResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = null
                };
            }
        }
        public async ValueTask<PlayerResponeDto> GetAllPlayer()
        {
            try
            {
                var players = _dbContext.Players.ToList();

                var playerlist = new List<ReadPlayerDto>();
                foreach (var player in players)
                {
                    var PlayerRespone = new ReadPlayerDto
                    {
                        PlayerAge = player.PlayerAge,
                        PlayerId = player.PlayerId,
                        PlayerHealth = player.PlayerHealth,
                        PlayerImage = player.PlayerImage,
                        PlayerNationality = player.PlayerNationality,
                        PlayerName = player.PlayerName,
                        PlayerPosition = player.PlayerPosition,
                        PlayerSalary = player.PlayerSalary,
                        PlayerSkill = player.PlayerSkill,
                        PlayerValue = player.PlayerValue,
                        ClubId = player.ClubId,
                        PlayerStatus = player.PlayerStatus,
                        Shirtnumber = player.Shirtnumber,
                        height = player.height,
                        weight = player.weight,
                        leg = player.leg,

                    };
                    playerlist.Add(PlayerRespone);
                }
                return new PlayerResponeDto
                {
                    EC = 0,
                    EM = "Get all Player Success",
                    DT = playerlist
                };
               
            }
            catch (Exception ex)
            {
                return new PlayerResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = null
                };
            }
        }
        public async ValueTask<PlayerResponeDto> GetPlayerById(int PlayerId)
        {
            try
            {
                var player = await  _dbContext.Players.FindAsync(PlayerId);
                if (player == null)
                {
                    return new PlayerResponeDto
                    {
                        EC = 1,
                        EM = "Not found Player",
                        DT = null
                    };
                }
                var PlayerRespone = new ReadPlayerDto
                {
                    PlayerAge = player.PlayerAge,
                    PlayerId = player.PlayerId,
                    PlayerHealth = player.PlayerHealth,
                    PlayerImage = player.PlayerImage,
                    PlayerNationality = player.PlayerNationality,
                     PlayerName = player.PlayerName,
                    PlayerPosition = player.PlayerPosition,
                    PlayerSalary = player.PlayerSalary,
                    PlayerSkill = player.PlayerSkill,
                    PlayerValue = player.PlayerValue,
                    ClubId = player.ClubId,
                    PlayerStatus = player.PlayerStatus,
                    Shirtnumber = player.Shirtnumber,
                    height = player.height,
                    weight = player.weight,
                    leg = player.leg,

                };
                return new PlayerResponeDto
                {
                    EC = 0,
                    EM = "find Player Sucess",
                    DT = PlayerRespone
                };
            }
            catch (Exception ex)
            {
                return new PlayerResponeDto
                {
                    EC = -1,
                    EM = ex.Message,
                    DT = null
                };
            }
        }




    }
}
