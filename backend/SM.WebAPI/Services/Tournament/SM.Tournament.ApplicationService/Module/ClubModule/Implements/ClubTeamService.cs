using Microsoft.Extensions.Logging;

using SM.Tournament.ApplicationService.Module.ClubModule.Abtracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Tournament.Dtos;
using SM.Tournament.Domain;
using SM.Tournament.Infrastructure;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.Dtos.ClubTeamDtos;
using SM.Tournament.Domain.Club;

namespace SM.Tournament.ApplicationService.Module.ClubModule.Implements
{
    public class ClubTeamService : ClubServiceBase, IClubTeamService
    {
        public ClubTeamService(ILogger<ClubTeamService> logger, TournamentDBContext dbContext) : base(logger, dbContext)
        {

        }
        public async Task<TournamentResponeDto> CreateClubTeam(CreateClubTeamDto createClubTeamDto)
        {
            try
            {
                var club = new ClubTeam
                {
                    ClubName = createClubTeamDto.ClubName,
                    ClubDescription = createClubTeamDto.ClubDescription,
                    UserId = createClubTeamDto.UserId,
                    ClubLogo = "",
                    ClubBanner = "",
                    Budget = 0,
                    CoachId = 0
                };
                _dbContext.Clubs.Add(club);
                await _dbContext.SaveChangesAsync();

                return new TournamentResponeDto
                {
                    EC = 0,
                    EM = "Create club success",
                    DT = null
                };

            }
            catch
            {
                return new TournamentResponeDto
                {
                    EC = 1,
                    EM = "Create club fail",
                    DT = null
                };

            }
        }
        public async Task<TournamentResponeDto> UpdateClubTeam(UpdateClubTeamDto updateClubTeamDto)
        {
            try
            {
                var existClub = await _dbContext.Clubs.FindAsync(updateClubTeamDto.ClubId);
                if (existClub == null)
                {
                    return new TournamentResponeDto
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
                existClub.CoachId = updateClubTeamDto.CoachId;
                existClub.Budget = updateClubTeamDto.Budget;

                await _dbContext.SaveChangesAsync();

                return new TournamentResponeDto
                {
                    EC = 0,
                    EM = "Update club success",
                    DT = null
                };
            }
            catch
            {
                return new TournamentResponeDto
                {
                    EC = 1,
                    EM = "Update club fail",
                    DT = null
                };
            }
        }
        public async Task<TournamentResponeDto> RemoveClubTeam(int clubId)
        {
            try
            {
                var club = await _dbContext.Clubs.FindAsync(clubId);
                if (club == null)
                {
                    return new TournamentResponeDto
                    {
                        EC = 1,
                        EM = "CLub not found",
                        DT = null
                    };
                }
                _dbContext.Clubs.Remove(club);
                await _dbContext.SaveChangesAsync();

                return new TournamentResponeDto
                {
                    EC = 0,
                    EM = "Remove club success",
                    DT = null
                };
            }
            catch
            {
                return new TournamentResponeDto
                {
                    EC = 1,
                    EM = "Remove club fail",
                    DT = null
                };
            }
        }
        public async ValueTask<IEnumerable<ClubTeam>> GetAllClubTeam()
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
                    CoachId = x.CoachId
                }).ToList();

                return clubList;
            }
            catch
            {
                return null;
            }
        }
        public async ValueTask<TournamentResponeDto> GetClubTeamById(int clubId)
        {
            try
            {
                var club = await _dbContext.Clubs.FindAsync(clubId);
                if (club == null)
                {
                    return new TournamentResponeDto
                    {
                        EC = 1,
                        EM = "Tournament not found",
                        DT = null
                    };
                }
                return new TournamentResponeDto
                {
                    EC = 0,
                    EM = "Get club success",
                    DT = club
                };
            }
            catch
            {
                return new TournamentResponeDto
                {
                    EC = 1,
                    EM = "Get club fail",
                    DT = null
                };
            }
        }

   
    }
}
