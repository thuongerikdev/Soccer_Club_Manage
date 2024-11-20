using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
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
        public  async Task<TournamentResponeDto> GetListPlayerVote(int minigameID)
        {
            var minigame = _dbContext.Votes.Where(x => x.MinigameID == minigameID).ToList();
            int matchesID = minigame.Select(x => x.MatchID).FirstOrDefault();
            var match = _dbContext.MatchesStatistics.Where(x => x.MatchesID == matchesID).ToList();
            int lineup = match.Select(x => x.LineUpID).FirstOrDefault();
            var playerLineUP = _dbContext.PlayerLineUps.Where(x => x.LineUpID == lineup).ToList();

            var ListPlayer = new List<PlayerVoteDto>();

            foreach (var item in playerLineUP)
            {
                var player = _dbContext.ClubPlayers.Where(x => x.PlayerID == item.PlayerID).FirstOrDefault();

                var chosen = new PlayerVoteDto
                {
                    selection = player.PlayerName,
                    count = 0
                };
                ListPlayer.Add(chosen);

            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "success",
                Data = ListPlayer
            };


        }

    }
}
