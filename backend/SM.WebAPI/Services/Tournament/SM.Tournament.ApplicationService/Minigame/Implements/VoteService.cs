using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Vote;
using SM.Tournament.Domain.Minigame;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto;
using SM.Tournament.Dtos.MinigameDto.Vote;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements
{
    public class VoteService : TournamentServiceBase, IVoteService
    {
        public VoteService(ILogger<VoteService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task<TournamentResponeDto> CreateVote(CreateVoteDto createVoteDto)
        {
            try
            {
                var vote = new Votes
                {
                    MinigameID = createVoteDto.MinigameID,
                    UserID = createVoteDto.UserID,
                    VoteDate = createVoteDto.VoteDate,

                    MatchID = createVoteDto.MatchID,
                    Selection = createVoteDto.Selection,


                };
                var exits = _dbContext.Votes.Where(x => x.MinigameID == createVoteDto.MinigameID && x.UserID == createVoteDto.UserID).FirstOrDefault();
                if (exits != null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "User has already voted",
                        Data = null
                    };
                }

                _dbContext.Votes.Add(vote);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Vote Success",
                    Data = null
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
        public async Task<TournamentResponeDto> DeleteVote(int voteID)
        {
            try
            {
                var vote = await _dbContext.Votes.FindAsync(voteID);
                if (vote == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Vote not found",
                        Data = null
                    };
                }
                _dbContext.Votes.Remove(vote);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Vote Success",
                    Data = null
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
        public async Task<TournamentResponeDto> GetVote(int voteID)
        {
            try
            {
                var vote = _dbContext.Votes.Find(voteID);
                if (vote == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Vote not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = vote
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
        public async Task<TournamentResponeDto> UpdateVote(UpdateVoteDto updateVoteDto)
        {
            try
            {
                var vote = await _dbContext.Votes.FindAsync(updateVoteDto.VoteID);
                if (vote == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Vote not found",
                        Data = null
                    };
                }
                vote.MinigameID = updateVoteDto.MinigameID;
                vote.UserID = updateVoteDto.UserID;
                vote.VoteDate = updateVoteDto.VoteDate;
                vote.MatchID = updateVoteDto.MatchID;
                vote.Selection = updateVoteDto.Selection;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Vote Success",
                    Data = null
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
        
        public async Task<TournamentResponeDto> GetVotesByMinigame(int minigameID)
        {
            try
            {
                var vote = _dbContext.Votes.Where(x => x.MinigameID == minigameID).ToList();
                if (vote == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Vote not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = vote
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
        public async Task<TournamentResponeDto> GetAllVotes()
        {
            var votes = _dbContext.Votes.ToList();
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get All Votes Success",
                Data = votes
            };

        }
        public async Task<TournamentResponeDto> GetVotesByUser(int userID)
        {
            try
            {
                var vote = _dbContext.Votes.Where(x => x.UserID == userID).ToList();
                if (vote == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Vote not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = vote
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
        public async Task<TournamentResponeDto> GetVotesByMatch(int matchID)
        {
            try
            {
                var vote = _dbContext.Votes.Where(x => x.MatchID == matchID).ToList();
                if (vote == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Vote not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = vote
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
        public async Task<TournamentResponeDto> GetVotebyID(int voteID)
        {
            try
            {
                var vote = _dbContext.Votes.Where(x => x.VoteID == voteID).ToList();
                if (vote == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Vote not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "success",
                    Data = vote
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
        public async Task<TournamentResponeDto> GetListPlayerVote(int minigameID)
        {
            var minigame = _dbContext.Votes.Where(x => x.MinigameID == minigameID).ToList();
            int matchesID = minigame.Select(x => x.MatchID).FirstOrDefault();

            var matchRes = _dbContext.Matches.Where(x => x.MatchesID == matchesID).FirstOrDefault();
            int teamA = matchRes.TeamA;
            int teamB = matchRes.TeamB;

            var matchTeamA = _dbContext.MatchesStatistics.Where(x => x.MatchesID == matchesID && x.ClubID == teamA).ToList();
            var matchTeamB = _dbContext.MatchesStatistics.Where(x => x.MatchesID == matchesID && x.ClubID == teamB).ToList();

            int lineupTeamA = matchTeamA.Select(x => x.LineUpID).FirstOrDefault();
            int lineupTeamB = matchTeamB.Select(x => x.LineUpID).FirstOrDefault();

            var playerLineUPTeamA = _dbContext.PlayerLineUps.Where(x => x.LineUpID == lineupTeamA).ToList();
            var playerLineUPTeamB = _dbContext.PlayerLineUps.Where(x => x.LineUpID == lineupTeamB).ToList();

            var listPlayer = new List<PlayerVoteDto>();

            // Fetch players for Team A
            foreach (var item in playerLineUPTeamA)
            {
                var player = _dbContext.ClubPlayers.Where(x => x.PlayerID == item.PlayerID).FirstOrDefault();
                if (player != null)
                {
                    var chosen = new PlayerVoteDto
                    {
                        selection = player.PlayerID,
                        count = 0
                    };
                    listPlayer.Add(chosen);
                }
            }

            // Fetch players for Team B
            foreach (var item in playerLineUPTeamB)
            {
                var player = _dbContext.ClubPlayers.Where(x => x.PlayerID == item.PlayerID).FirstOrDefault();
                if (player != null)
                {
                    var chosen = new PlayerVoteDto
                    {
                        selection = player.PlayerID,
                        count = 0
                    };
                    listPlayer.Add(chosen);
                }
            }

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "success",
                Data = listPlayer
            };
        }
        public async Task<TournamentResponeDto> ListPlayerCanVote(int minigameID)
        {
            var minigame = _dbContext.Minigames.FirstOrDefault(x => x.MinigameID == minigameID);
            int matchesID = minigame.MatchesID;

            var matchRes = _dbContext.Matches.Where(x => x.MatchesID == matchesID).FirstOrDefault();
            int teamA = matchRes.TeamA;
            int teamB = matchRes.TeamB;

            var matchTeamA = _dbContext.MatchesStatistics.Where(x => x.MatchesID == matchesID && x.ClubID == teamA).ToList();
            var matchTeamB = _dbContext.MatchesStatistics.Where(x => x.MatchesID == matchesID && x.ClubID == teamB).ToList();

            int lineupTeamA = matchTeamA.Select(x => x.LineUpID).FirstOrDefault();
            int lineupTeamB = matchTeamB.Select(x => x.LineUpID).FirstOrDefault();

            var playerLineUPTeamA = _dbContext.PlayerLineUps.Where(x => x.LineUpID == lineupTeamA).ToList();
            var playerLineUPTeamB = _dbContext.PlayerLineUps.Where(x => x.LineUpID == lineupTeamB).ToList();

            var listPlayer = new List<PlayerVoteDto>();

            // Fetch players for Team A
            foreach (var item in playerLineUPTeamA)
            {
               
                if (item != null)
                {
                    var chosen = new PlayerVoteDto
                    {
                        selection = item.PlayerID,
                        count = 0
                    };
                    listPlayer.Add(chosen);
                }
            }

            // Fetch players for Team B
            foreach (var item in playerLineUPTeamB)
            {
                if (item != null)
                {
                    var chosen = new PlayerVoteDto
                    {
                        selection = item.PlayerID,
                        count = 0
                    };
                    listPlayer.Add(chosen);
                }
            }

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "success",
                Data = listPlayer
            };
        }

    }
}
