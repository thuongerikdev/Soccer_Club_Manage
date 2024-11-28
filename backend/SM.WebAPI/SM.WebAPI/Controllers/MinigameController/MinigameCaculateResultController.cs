using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;
using SM.Tournament.Dtos;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Caculation;
using SM.Tournament.Dtos.MinigameDto;

namespace SM.WebAPI.Controllers.MinigameController
{
    [Route("api/minigame")]
    [ApiController]
    public class MinigameCaculateResultController : Controller
    {
        private readonly IMinigameUse _minigameuse;

        public MinigameCaculateResultController(IMinigameUse minigameuse)
        {
            _minigameuse = minigameuse;

        }
        [HttpGet("minigameResult/{type}")]
        public async Task<IActionResult> getMiniGameResult(string type, [FromQuery] CaculateResultDto caculateResult)
        {
            try
            {
                if (caculateResult.MinigameID == null)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Invalid request: ReadMatchesStatisticDto is null.",
                        Data = null
                    });
                }
                var minigame = _minigameuse.chooseType(type);

                if (minigame == null)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Invalid request: ReadMatchesStatisticDto is null.",
                        Data = null
                    });
                }
                //if(type == "vote")
                //{
                //    var result = 
                //}

                var result = await minigame.MinigameResult(caculateResult.half, caculateResult.MinigameID, type, caculateResult.topic);


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
        [HttpGet("caculateVote/{type}")]
        public async Task<IActionResult> caculateVote(string type, int MinigameID)
        {
            try
            {

                var minigame = _minigameuse.chooseVoteType(type);

                if (minigame == null)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Invalid request: ReadMatchesStatisticDto is null.",
                        Data = null
                    });
                }
                //if(type == "vote")
                //{
                //    var result = 
                //}

                var result = await minigame.CaculateVote(MinigameID);
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
    
  