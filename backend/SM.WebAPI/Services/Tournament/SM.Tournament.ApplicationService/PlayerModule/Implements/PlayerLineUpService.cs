using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.PlayerModule.Abtracts;
using SM.Tournament.Domain.Player;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerLineUp;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.PlayerModule.Implements
{
    public class PlayerLineUpService : TournamentServiceBase , IPlayerLineUpService
    {
        public PlayerLineUpService (ILogger<PlayerLineUpService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreatePlayerLineUps(List<CreatePlayerLineUpDto> createPlayerLineUpDtos)
        {
            // Lấy LineUpID từ các DTO
            var lineUpIDs = createPlayerLineUpDtos.Select(dto => dto.LineUpID).Distinct().ToList();

            // Tìm và xóa các PlayerLineUp cũ theo LineUpID
            var oldPlayerLineUps = await _dbContext.PlayerLineUps
                .Where(p => lineUpIDs.Contains(p.LineUpID))
                .ToListAsync();

            _dbContext.PlayerLineUps.RemoveRange(oldPlayerLineUps); // Xóa tất cả các player lineups cũ

            // Tạo danh sách PlayerLineUp mới
            var playerLineUps = new List<PlayerLineUp>();

            foreach (var createPlayerLineUpDto in createPlayerLineUpDtos)
            {
                var playerLineup = new PlayerLineUp
                {
                    PlayerID = createPlayerLineUpDto.PlayerID,
                    LineUpID = createPlayerLineUpDto.LineUpID,
                    Status = createPlayerLineUpDto.Status,
                    CreatedDate = createPlayerLineUpDto.CreatedDate,
                    Position = createPlayerLineUpDto.Position,
                    IsCaptain = createPlayerLineUpDto.IsCaptain
                };

                playerLineUps.Add(playerLineup);
            }

            _dbContext.PlayerLineUps.AddRange(playerLineUps); // Thêm tất cả player lineups mới

            await _dbContext.SaveChangesAsync();

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Create Player LineUps Success",
                Data = playerLineUps.Select(p => p.PlayerLineUpID).ToList() // Trả về tất cả các ID đã tạo
            };
        }
        public async Task<TournamentResponeDto> UpdatePlayerLineUp (UpdatePlayerLineUpDto updatePlayerLineUpDto)
        {
            var playerlineup = await _dbContext.PlayerLineUps.FindAsync(updatePlayerLineUpDto.PlayerLineUpID);
            if (playerlineup == null) throw new ArgumentException("Player LineUp not found");
            playerlineup.PlayerID = updatePlayerLineUpDto.PlayerID;
            playerlineup.LineUpID = updatePlayerLineUpDto.LineUpID;
            playerlineup.Status = updatePlayerLineUpDto.Status;
            playerlineup.CreatedDate = updatePlayerLineUpDto.CreatedDate;
            playerlineup.Position = updatePlayerLineUpDto.Position;
            playerlineup.IsCaptain = updatePlayerLineUpDto.IsCaptain;
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Update Player LineUp Success",
                Data = playerlineup.PlayerLineUpID
            };
        }
        public async Task<TournamentResponeDto> DeletePlayerLineUp(int PlayerLineUpID)
        {
            var playerlineup = await _dbContext.PlayerLineUps.FindAsync(PlayerLineUpID);
            if (playerlineup == null) throw new ArgumentException("Player LineUp not found");
            _dbContext.PlayerLineUps.Remove(playerlineup);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Delete Player LineUp Success",
                Data = null
            };
        }
        public async Task<TournamentResponeDto> GetPlayerLineUp()
        {
            var playerlineup = _dbContext.PlayerLineUps.ToList();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Player LineUp Success",
                Data = playerlineup
            };
        }
        public async Task<TournamentResponeDto> GetPlayerLineUpByLineUp(int LineUpID)
        {
            var playerlineup = _dbContext.PlayerLineUps.Where(x => x.LineUpID == LineUpID).ToList();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Player LineUp By Club Success",
                Data = playerlineup
            };
        }
    }
}
