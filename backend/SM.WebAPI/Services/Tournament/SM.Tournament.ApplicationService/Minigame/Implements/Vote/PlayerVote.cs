using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Vote;

//using SM.Tournament.ApplicationService.Minigame.Implements.Predict.PredictMatches;
using SM.Tournament.Domain.Player;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.Minigame.Implements.Vote
{
    public class PlayerVote : TournamentServiceBase, ICaculateVote
    {
        private IVoteService _voteService;
        public PlayerVote(ILogger<PlayerVote> logger, TournamentDbContext dbContext, IVoteService voteService) : base(logger, dbContext)
        {
            _voteService = voteService;
        }
        public async Task<TournamentResponeDto> CaculateVote ( int minigameID)

        {
            var listPlayerJoinResponse = await _voteService.ListPlayerCanVote(minigameID);
            var listPlayerJoin = listPlayerJoinResponse.Data as List<PlayerVoteDto>;

            var allvote = _dbContext.Votes.Where(x => x.MinigameID == minigameID).ToList();
            foreach (var item in allvote)
            {
                var player = listPlayerJoin.Where(x => x.selection == item.Selection).FirstOrDefault();
                if (player != null)
                {
                    player.count++;
                }

            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Votes counted successfully.",
                Data = listPlayerJoinResponse // Trả về danh sách cầu thủ cùng với số bình chọn
            };
        }
    }
}
