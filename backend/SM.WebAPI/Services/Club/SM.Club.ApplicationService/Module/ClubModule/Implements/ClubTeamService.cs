using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SM.Club.ApplicationService.Module.ClubModule.Abtracts;
using SM.Club.Infrastructure;
using SM.Club.Dtos;
using SM.Club.Dtos.ClubTeamDtos;
using SM.Club.Domain;
using SM.Club.ApplicationService.Common;
using SM.Club.Domain.Club;



namespace SM.Club.ApplicationService.Module.ClubModule.Implements
{
    public class ClubTeamService : ClubServiceBase, IClubTeamService
    {
        public ClubTeamService(ILogger<ClubTeamService> logger, ClubDbContext dbContext) : base(logger, dbContext)
        {

        }
        public async Task<ClubResponeDto> CreateClubTeam(CreateClubTeamDto createClubTeamDto)
        {
            try
            {
                var club = new ClubTeam
                {
                    ClubName = createClubTeamDto.ClubName,
                    ClubDescription = createClubTeamDto.ClubDescription,
                    UserId = createClubTeamDto.UserId,
                    ClubLogo = createClubTeamDto.ClubLogo,
                    ClubAge = createClubTeamDto.ClubAge,
                    ClubBanner = createClubTeamDto.ClubBanner,
                    ClubLevel = createClubTeamDto.ClubLevel,
                    Budget = createClubTeamDto.Budget,
                };
                _dbContext.Clubs.Add(club);
                await _dbContext.SaveChangesAsync();

                return new ClubResponeDto
                {
                    EC = 0,
                    EM = "Create club success",
                    DT = null
                };

            }
            catch
            {
                return new ClubResponeDto
                {
                    EC = 1,
                    EM = "Create club fail",
                    DT = null
                };

            }
        }
        public async Task<ClubResponeDto> UpdateClubTeam(int ClubId , UpdateClubTeamDto updateClubTeamDto)
        {
            try
            {
                var existClub = _dbContext.Clubs.FirstOrDefault(x => x.ClubId == ClubId);
                if (existClub == null)
                {
                    return new ClubResponeDto
                    {
                        EC = 1,
                        EM = "Tournament not found",
                        DT = null
                    };
                }
                existClub.ClubName = updateClubTeamDto.ClubName;
                existClub.ClubDescription = updateClubTeamDto.ClubDescription;
                existClub.UserId = updateClubTeamDto.UserId;
                existClub.ClubBanner = updateClubTeamDto.ClubBanner;
                existClub.ClubLogo = updateClubTeamDto.ClubLogo;
                existClub.Budget = updateClubTeamDto.Budget;
                existClub.ClubLevel = updateClubTeamDto.ClubLevel;
                existClub.ClubAge = updateClubTeamDto.ClubAge;

                await _dbContext.SaveChangesAsync();

                return new ClubResponeDto
                {
                    EC = 0,
                    EM = "Update club success",
                    DT = null
                };
            }
            catch
            {
                return new ClubResponeDto
                {
                    EC = 1,
                    EM = "Update club fail",
                    DT = null
                };
            }
        }
        public async Task<ClubResponeDto> RemoveClubTeam(int clubId)
        {
            try
            {
                var club = await _dbContext.Clubs.FindAsync(clubId);
                if (club == null)
                {
                    return new ClubResponeDto
                    {
                        EC = 1,
                        EM = "CLub not found",
                        DT = null
                    };
                }
                _dbContext.Clubs.Remove(club);
                await _dbContext.SaveChangesAsync();

                return new ClubResponeDto
                {
                    EC = 0,
                    EM = "Remove club success",
                    DT = null
                };
            }
            catch
            {
                return new ClubResponeDto
                {
                    EC = 1,
                    EM = "Remove club fail",
                    DT = null
                };
            }
        }
        public async ValueTask<ClubResponeDto> GetAllClubTeam()
        {
            try
            {

                var club = _dbContext.Clubs.ToList();
                var clubList = club.Select(x => new ClubTeam
                {
                    ClubId = x.ClubId,
                    ClubName = x.ClubName,
                    ClubDescription = x.ClubDescription,
                    UserId = x.UserId,
                    ClubLogo = x.ClubLogo,
                    ClubBanner = x.ClubBanner,
                    Budget = x.Budget,
                    ClubLevel = x.ClubLevel,
                    ClubAge = x.ClubAge,
                }).ToList();

                return new ClubResponeDto
                {
                    EC = 0 ,
                    EM = "sucess" ,
                    DT = clubList
                };
            }
            catch
            {
                return null;
            }
        }
        public async ValueTask<ClubResponeDto> GetClubTeamById(int clubId)
        {
            try
            {
                var club = await _dbContext.Clubs.FindAsync(clubId);
                if (club == null)
                {
                    return new ClubResponeDto
                    {
                        EC = 1,
                        EM = "Tournament not found",
                        DT = null
                    };
                }
                var ResponeClub = new GetClubTeamDto
                {
                    ClubId = club.ClubId,
                    ClubName = club.ClubName,
                    ClubDescription = club.ClubDescription,
                    UserId = club.UserId,
                    ClubLogo = club.ClubLogo,
                    ClubBanner = club.ClubBanner,
                    Budget = club.Budget

                };
                return new ClubResponeDto
                {
                    EC = 0,
                    EM = "Get club success",
                    DT = ResponeClub
                };
            }
            catch
            {
                return new ClubResponeDto
                {
                    EC = 1,
                    EM = "Get club fail",
                    DT = null
                };
            }
        }

   
    }
}
