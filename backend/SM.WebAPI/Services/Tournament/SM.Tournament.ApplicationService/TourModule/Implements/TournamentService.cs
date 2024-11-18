using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using SM.Tournament.Domain.Tournament;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.TournamentDto.Tournament;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.TourModule.Implements
{
    public class TournamentService: TournamentServiceBase , ITournamentService
    {
        public TournamentService (ILogger<ITournamentService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task <TournamentResponeDto> createTournament (CreateTournamentDto createTournamentDto)
        {
            var tour = new TournamentBase
            {
                TournamentContact = createTournamentDto.TournamentContact,
                TournamentDescription = createTournamentDto.TournamentDescription,
                TournamentLocation = createTournamentDto.TournamentLocation,
                TournamentName = createTournamentDto.TournamentName,
                TournamentStatus = createTournamentDto.TournamentStatus,
                TournamentType = createTournamentDto.TournamentType,
                TournamentOrganizer = createTournamentDto.TournamentOrganizer,
                TournamentPrice = createTournamentDto.TournamentPrice,
                StartDate = createTournamentDto.StartDate,
                EndDate = createTournamentDto.EndDate,
                
            };

            _dbContext.Tournaments.Add(tour);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Create Tournament Success",
                Data = tour.TournamentID
            };
        }
        public async Task<TournamentResponeDto> deleteTournament(int tournamentID)
        {
            var tour = _dbContext.Tournaments.FirstOrDefault(x => x.TournamentID == tournamentID);
            if (tour == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament not found",
                    Data = null
                };
            }
            _dbContext.Tournaments.Remove(tour);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Delete Tournament Success",
                Data = null
            };
        }
        public async Task<TournamentResponeDto> updateTournament(UpdateTournamentDto updateTournamentDto)
        {
            var tour = _dbContext.Tournaments.FirstOrDefault(x => x.TournamentID == updateTournamentDto.TournamentID);
            if (tour == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament not found",
                    Data = null
                };
            }
            tour.TournamentContact = updateTournamentDto.TournamentContact;
            tour.TournamentDescription = updateTournamentDto.TournamentDescription;
            tour.TournamentLocation = updateTournamentDto.TournamentLocation;
            tour.TournamentName = updateTournamentDto.TournamentName;
            tour.TournamentStatus = updateTournamentDto.TournamentStatus;
            tour.TournamentType = updateTournamentDto.TournamentType;
            tour.TournamentOrganizer = updateTournamentDto.TournamentOrganizer;
            tour.TournamentPrice = updateTournamentDto.TournamentPrice;
            tour.StartDate = updateTournamentDto.StartDate;
            tour.EndDate = updateTournamentDto.EndDate;
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Update Tournament Success",
                Data = null
            };
        }
        public async Task<TournamentResponeDto> getTournament(int tournamentID)
        {
            var tour = _dbContext.Tournaments.FirstOrDefault(x => x.TournamentID == tournamentID);
            if (tour == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Tournament not found",
                    Data = null
                };
            }
            var readTournamentDto = new ReadTournamentDto
            {
                TournamentContact = tour.TournamentContact,
                TournamentDescription = tour.TournamentDescription,
                TournamentLocation = tour.TournamentLocation,
                TournamentName = tour.TournamentName,
                TournamentStatus = tour.TournamentStatus,
                TournamentType = tour.TournamentType,
                TournamentOrganizer = tour.TournamentOrganizer,
                TournamentPrice = tour.TournamentPrice,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                TournamentID = tour.TournamentID
            };
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Tournament Success",
                Data = readTournamentDto
            };
        }
        public async Task<TournamentResponeDto> getTournaments()
        {
            var tours = _dbContext.Tournaments.ToList();
            var readTournamentDtos = new List<ReadTournamentDto>();
            foreach (var tour in tours)
            {
                var readTournamentDto = new ReadTournamentDto
                {
                    TournamentContact = tour.TournamentContact,
                    TournamentDescription = tour.TournamentDescription,
                    TournamentLocation = tour.TournamentLocation,
                    TournamentName = tour.TournamentName,
                    TournamentStatus = tour.TournamentStatus,
                    TournamentType = tour.TournamentType,
                    TournamentOrganizer = tour.TournamentOrganizer,
                    TournamentPrice = tour.TournamentPrice,
                    StartDate = tour.StartDate,
                    EndDate = tour.EndDate,
                    TournamentID = tour.TournamentID
                };
                readTournamentDtos.Add(readTournamentDto);
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get All Tournament Success",
                Data = readTournamentDtos
            };
        }

    }
}
