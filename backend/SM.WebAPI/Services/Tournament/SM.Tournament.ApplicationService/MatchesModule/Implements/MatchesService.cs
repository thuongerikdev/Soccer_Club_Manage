using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts;
using SM.Tournament.ApplicationService.PlayerModule.Implements;
using SM.Tournament.Domain.Match;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.Matches;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.MatchesModule.Implements
{
    public class MatchesService : TournamentServiceBase , IMatchesService
    {
        public MatchesService(ILogger<MatchesService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreateMatches(CreateMatchesDto createMatchesDto)
        {
            try
            {
                var match = new Matches
                {
                    MatchesDescription = createMatchesDto.MatchesDescription,
                    MatchesName = createMatchesDto.MatchesName,
                    StartTime = createMatchesDto.StartTime,
                    EndTime = createMatchesDto.EndTime,
                    Stadium = createMatchesDto.Stadium,
                    TeamA = createMatchesDto.TeamA,
                    TeamB = createMatchesDto.TeamB,
                    TournamentID = createMatchesDto.TournamentID

                };
                _dbContext.Matches.Add(match);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Match Success",
                    Data = match.MatchesID
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
        public async Task<TournamentResponeDto> UpdateMatches(UpdateMatchesDto updateMatchesDto)
        {
            try
            {
                var match = await _dbContext.Matches.FindAsync(updateMatchesDto.MatchesID);
                if (match == null) throw new ArgumentException("Match not found");
                match.MatchesDescription = updateMatchesDto.MatchesDescription;
                match.MatchesName = updateMatchesDto.MatchesName;
                match.StartTime = updateMatchesDto.StartTime;
                match.EndTime = updateMatchesDto.EndTime;
                match.Stadium = updateMatchesDto.Stadium;
                match.TeamA = updateMatchesDto.TeamA;
                match.TeamB = updateMatchesDto.TeamB;
                match.TournamentID = updateMatchesDto.TournamentID;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Match Success",
                    Data = match.MatchesID
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
        public async Task<TournamentResponeDto> DeleteMatches(int MatchID)
        {
            try
            {
                
                var match =  await _dbContext.Matches.FindAsync(MatchID);
                if (match == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match not found",
                        Data = null
                    };
                }
                _dbContext.Matches.Remove(match);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Match Success",
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
        public async Task<TournamentResponeDto> GetMatches(int MatchID)
        {
            try
            {
                var match = _dbContext.Matches.Find(MatchID);
                if (match == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Match not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Match Success",
                    Data = match
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
        public async Task<TournamentResponeDto> GetAllMatches()
        {
            try
            {
                var matches = _dbContext.Matches.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Matches Success",
                    Data = matches
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
        public async  Task<TournamentResponeDto> GetMatchesByTeamA(int TeamA)
        {
            try
            {
                var matches = _dbContext.Matches.Where(x => x.TeamA == TeamA).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Matches By Tournament ID Success",
                    Data = matches
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
        public async Task<TournamentResponeDto> GetMatchesByTeamB(int TeamB)
        {
            try
            {
                var matches = _dbContext.Matches.Where(x => x.TeamB == TeamB).ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Matches By Tournament ID Success",
                    Data = matches
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

    }
}
