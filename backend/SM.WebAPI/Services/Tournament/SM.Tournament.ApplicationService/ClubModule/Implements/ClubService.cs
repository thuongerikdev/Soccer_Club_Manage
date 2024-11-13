using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Dtos.ClubDto.Club;
using SM.Tournament.Dtos;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Tournament.Domain.Club.Club;
using SM.Tournament.ApplicationService.ClubModule.Abtracts;

namespace SM.Tournament.ApplicationService.ClubModule.Implements
{
    public class ClubService : TournamentServiceBase, IClubService
    {
        public ClubService(ILogger<ClubService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreateClubTeam(CreateClubDto createClubTeamDto)
        {
            try
            {
                var club = new ClubTeam
                {
                    ClubName = createClubTeamDto.ClubName,
                    ClubBanner = createClubTeamDto.ClubBanner,
                    ClubAge = createClubTeamDto.ClubAge,
                    ClubLevel = createClubTeamDto.ClubLevel,
                    ClubDescription = createClubTeamDto.ClubDescription,
                    ClubLogo = createClubTeamDto.ClubLogo,
                    UserID = createClubTeamDto.UserID
                };
                _dbContext.ClubTeams.Add(club);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Club Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create Club Error");
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Create Club Error",
                    Data = null
                };
            }

        }
        public async Task<TournamentResponeDto> UpdateClubTeam(UpdateClubDto updateClubTeamDto)
        {
            try
            {
                var existClub = await _dbContext.ClubTeams.FindAsync(updateClubTeamDto.ClubId);
                if (existClub == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Club not found",
                        Data = null
                    };
                }
                existClub.ClubName = updateClubTeamDto.ClubName;
                existClub.ClubBanner = updateClubTeamDto.ClubBanner;
                existClub.ClubAge = updateClubTeamDto.ClubAge;
                existClub.ClubLevel = updateClubTeamDto.ClubLevel;
                existClub.ClubDescription = updateClubTeamDto.ClubDescription;
                existClub.ClubLogo = updateClubTeamDto.ClubLogo;
                existClub.UserID = updateClubTeamDto.UserID;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Club Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Club Error");
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Update Club Error",
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> RemoveClubTeam(int clubTeamId)
        {
            try
            {
                var existClub = await _dbContext.ClubTeams.FindAsync(clubTeamId);
                if (existClub == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Club not found",
                        Data = null
                    };
                }
                _dbContext.ClubTeams.Remove(existClub);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Remove Club Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Remove Club Error");
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Remove Club Error",
                    Data = null
                };
            }
        }
        public async ValueTask<TournamentResponeDto> GetAllClubTeam()
        {
            try
            {
                var clubTeams = _dbContext.ClubTeams.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Club Success",
                    Data = clubTeams
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get All Club Error");
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Get All Club Error",
                    Data = null
                };
            }
        }
        public async ValueTask<TournamentResponeDto> GetClubTeamById(int clubTeamId)
        {
            try
            {
                var clubTeam = await _dbContext.ClubTeams.FindAsync(clubTeamId);
                if (clubTeam == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Club not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Club Success",
                    Data = clubTeam
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get Club Error");
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Get Club Error",
                    Data = null
                };
            }
        }
        public async ValueTask<TournamentResponeDto> GetClubTeamByUserId(int UserID)
        {
            try
            {
                var clubTeams = _dbContext.ClubTeams.Where(x => x.UserID == UserID).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Club Success",
                    Data = clubTeams
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get Club Error");
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Get Club Error",
                    Data = null
                };
            }
        }
    }
}
