using Microsoft.Extensions.Logging;
using SM.Match.ApplicationService.Common;
using SM.Match.ApplicationService.Module.MatchesModule.Abtracts;
using SM.Match.Domain.Matches;
using SM.Match.Dtos;
using SM.Match.Dtos.MatchesDto.Matches;
using SM.Match.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Match.ApplicationService.Module.MatchesModule.Implements
{
    public class MatchesService: MatchServiceBase , IMatchesService
    {
        public MatchesService(ILogger<MatchesService> logger, MatchDbContext dbContext) : base(logger, dbContext)
        {

        }
        public async  Task<MatchResponeDto> CreateMatches(CreateMatchesDto createMatchesDto)
        {
            try
            {
                var matches = new Matches
                {
                    MatchesName = createMatchesDto.MatchesName,
                    MatchesDescription = createMatchesDto.MatchesDescription,
                    TournamentId = createMatchesDto.TournamentId,
                    Stadium = createMatchesDto.Stadium,
                    StartTime = createMatchesDto.StartTime,
                    EndTime = createMatchesDto.EndTime
                };
             
                _dbContext.Matches.Add(matches);
                await _dbContext.SaveChangesAsync();
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Matches Success",
                    Data = ""
                };
            }
            catch (Exception ex)
            {
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = ex.Message,
                    Data = ""
                };
            }
            
        }
        public async Task<MatchResponeDto> UpdateMatches(UpdateMatchesDto updateMatchesDto)
        {
           try {
                var existmatches =  await _dbContext.Matches.FindAsync(updateMatchesDto.MatchesId);
                if (existmatches == null)
                {
                    return new MatchResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Matches not found",
                        Data = ""
                    };
                }
                existmatches.MatchesName = updateMatchesDto.MatchesName;
                existmatches.MatchesDescription = updateMatchesDto.MatchesDescription;
                 existmatches.TournamentId = updateMatchesDto.TournamentId;
                existmatches.Stadium = updateMatchesDto.Stadium;
                existmatches.StartTime = updateMatchesDto.StartTime;
                existmatches.EndTime = updateMatchesDto.EndTime;
                existmatches.TeamWin = updateMatchesDto.TeamWin;
                existmatches.TeamLose = updateMatchesDto.TeamLose;
                await _dbContext.SaveChangesAsync();
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Matches Success",
                    Data = ""
                };
                
            }
            catch (Exception ex)
            {
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = ex.Message,
                    Data = ""
                };
            }

        }
        public  async Task<MatchResponeDto> RemoveMatches(int matchesId)
        {
            try
            {
                var matches = await _dbContext.Matches.FindAsync(matchesId);
                if (matches == null)
                {
                    return new MatchResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Matches not found",
                        Data = ""
                    };
                }
                _dbContext.Matches.Remove(matches);
                await _dbContext.SaveChangesAsync();
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Remove Matches Success",
                    Data = ""
                };
            }
            catch (Exception ex)
            {
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = ex.Message,
                    Data = ""
                };

            }
         

        }
        public async ValueTask<MatchResponeDto> GetAllMatches()
        {
            try
            {
                var matches = _dbContext.Matches.Select(x => new GetMatchesDto
                {
                    MatchesId = x.MatchesId,
                    MatchesName = x.MatchesName,
                    MatchesDescription = x.MatchesDescription,
                    TournamentId = x.TournamentId,
                    Stadium = x.Stadium,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    TeamWin = x.TeamWin,
                    TeamLose = x.TeamLose
                }).ToList();
                return new MatchResponeDto
                {
                    ErrorCode = 0 ,
                    ErrorMessage = "Get Matches Success",
                    Data = matches
                };
            }
            catch (Exception ex)
            {
                return null;

            }
           
        }
        public async ValueTask<MatchResponeDto> GetMatchesById(int matchesId)
        {
            try
            {
                var matches = await _dbContext.Matches.FindAsync(matchesId);
                if (matches == null)
                {
                    return new MatchResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Matches not found",
                        Data = null
                    };
                }
                return new MatchResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Matches Success",
                    Data = new GetMatchesDto
                    {
                        MatchesId = matches.MatchesId,
                        MatchesName = matches.MatchesName,
                        MatchesDescription = matches.MatchesDescription,
                        TournamentId = matches.TournamentId,
                        Stadium = matches.Stadium,
                        StartTime = matches.StartTime,
                        EndTime = matches.EndTime,
                        TeamWin = matches.TeamWin,
                        TeamLose = matches.TeamLose
                    }
                };
            }
            catch (Exception ex)
            {
                return new MatchResponeDto
                {
                    ErrorCode = -1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
    }


}
