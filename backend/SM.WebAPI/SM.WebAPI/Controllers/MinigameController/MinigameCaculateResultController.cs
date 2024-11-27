using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;

namespace SM.WebAPI.Controllers.MinigameController
{
    [Route("api/minigame")]
    [ApiController]
    public class MinigameCaculateResultController : Controller
    {
        private readonly IMinigameUse _minigameuse;
       
        public MinigameCaculateResultController(IMinigameUse minigameuse )
        {
            _minigameuse = minigameuse;

        }
        [HttpGet("minigameResult/{type}/{MiniGameID}/{half}/{topic}")]
        public async Task <IActionResult> getMiniGameResult ( int MiniGameID , int half , string type, string topic)
        {
            try
            {
                if (MiniGameID == null)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Invalid request: ReadMatchesStatisticDto is null.",
                        Data = null
                    });
                }
                var minigame =  _minigameuse.chooseType(type);

                var result = await minigame.MinigameResult(half, MiniGameID, type , topic);

                
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = result.ErrorMessage,
                        Data = null
                    });
                }
                else
                {
                    return Ok(new TournamentResponeDto
                    {
                        ErrorCode = 0,
                        ErrorMessage = "Get Statistic Success",
                        Data = result.Data
                    });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                });
            }
        }
    }
}
