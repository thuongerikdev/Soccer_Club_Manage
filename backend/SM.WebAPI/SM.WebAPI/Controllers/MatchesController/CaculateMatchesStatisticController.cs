using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts.Statistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;

namespace SM.WebAPI.Controllers.MatchesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaculateMatchesStatisticController : Controller
    {
        private readonly IMatchStatisticUse _matchStatisticUse;
        public CaculateMatchesStatisticController(IMatchStatisticUse matchStatisticUse)
        {
            _matchStatisticUse = matchStatisticUse;
        }
        [HttpGet("statistic/{type}")]
        public async Task <IActionResult>  GetStatistic(string type ,[FromQuery] ReadMatchesStatisticDto readMatchesStatisticDto)
        {
            try
            {
                if (readMatchesStatisticDto == null)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Invalid request: ReadMatchesStatisticDto is null.",
                        Data = null
                    });
                }
                var service = _matchStatisticUse.ChooseStatistic(type);

                var result = await service.getStatisTic(readMatchesStatisticDto);
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
