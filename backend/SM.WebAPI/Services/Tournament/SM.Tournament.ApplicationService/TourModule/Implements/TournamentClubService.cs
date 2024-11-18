using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using SM.Tournament.Domain.Tournament;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.TournamentDto.TournamentClub;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TourModule.Implements
{
    public class TournamentClubService : TournamentServiceBase, ITournamentClubService
    {
        public TournamentClubService(ILogger<ITournamentClubService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> createTournamentClub(CreateTournamentClubDto createTournamentClubDto)
        {
            var tourClub = new TournamentClub
            {
                ClubID = createTournamentClubDto.ClubID,
                TournamentID = createTournamentClubDto.TournamentID,
                Drawn = createTournamentClubDto.Drawn,
                Lost = createTournamentClubDto.Lost,
                Played = createTournamentClubDto.Played,
                Points = createTournamentClubDto.Points,
                Won = createTournamentClubDto.Won,
                Rank = createTournamentClubDto.Rank,



            };

            _dbContext.TournamentClubs.Add(tourClub);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Create Tournament Club Success",
                Data = tourClub.TournamentClubID
            };
        }
        public async Task<TournamentResponeDto> updateTournamentClub(UpdateTournamentClubDto updateTournamentClubDto)
        {
            var tourClub = _dbContext.TournamentClubs.FirstOrDefault(x => x.TournamentClubID == updateTournamentClubDto.TournamentClubID);
            if (tourClub == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament Club not found",
                    Data = null
                };
            }
            tourClub.ClubID = updateTournamentClubDto.ClubID;
            tourClub.TournamentID = updateTournamentClubDto.TournamentID;
            tourClub.Drawn = updateTournamentClubDto.Drawn;
            tourClub.Lost = updateTournamentClubDto.Lost;
            tourClub.Played = updateTournamentClubDto.Played;
            tourClub.Points = updateTournamentClubDto.Points;
            tourClub.Won = updateTournamentClubDto.Won;
            tourClub.Rank = updateTournamentClubDto.Rank;
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Update Tournament Club Success",
                Data = tourClub.TournamentClubID
            };
        }
        public async Task<TournamentResponeDto> deleteTournamentClub(int tournamentClubID)
        {
            var tourClub = _dbContext.TournamentClubs.FirstOrDefault(x => x.TournamentClubID == tournamentClubID);
            if (tourClub == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament Club not found",
                    Data = null
                };
            }
            _dbContext.TournamentClubs.Remove(tourClub);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Delete Tournament Club Success",
                Data = null
            };
        }
        public async Task<TournamentResponeDto> getTournamentClubById(int tournamentClubID)
        {
            var tourClub = _dbContext.TournamentClubs.FirstOrDefault(x => x.TournamentClubID == tournamentClubID);
            if (tourClub == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament Club not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Tournament Club Success",
                Data = tourClub
            };
        }
        public async Task<TournamentResponeDto> getTournamentClubByTournamentId(int tournamentID)
        {
            var tourClub = _dbContext.TournamentClubs.Where(x => x.TournamentID == tournamentID).ToList();
            if (tourClub == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament Club not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Tournament Club Success",
                Data = tourClub
            };
        }
        public async Task<TournamentResponeDto> getTournamentClubs()
        {
            var tourClubs = _dbContext.TournamentClubs.ToList();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Tournament Clubs Success",
                Data = tourClubs
            };
        }
        public async Task<TournamentResponeDto> getTournamentClubByClubId(int clubID)
        {
            var tourClub = _dbContext.TournamentClubs.Where(x => x.ClubID == clubID).ToList();
            if (tourClub == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament Club not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Tournament Club Success",
                Data = tourClub
            };
        }
        public async Task<TournamentResponeDto> getTournamentClub(int touraClubID)
        {
            var tourClub = _dbContext.TournamentClubs.FirstOrDefault(x => x.TournamentClubID == touraClubID);
            if (tourClub == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament Club not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Tournament Club Success",
                Data = tourClub
            };
        } 


      }
}
