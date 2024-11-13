using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.MatchesModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MatchDto.MatchesStatistic;

namespace SM.WebAPI.Controllers.MatchesController
{
    [Route("api/Matches")]
    [ApiController]
    public class MatchesStatisticController : Controller
    {
        private readonly IMatchesStatisticService _matchesStatisticService;
        public MatchesStatisticController(IMatchesStatisticService matchesStatisticService)
        {
            _matchesStatisticService = matchesStatisticService;
        }
        [HttpPost("createMatchesStatistic")]
        public async Task<IActionResult> CreateMatchesStatistic(CreateMatchesStatisticDto createMatchesStatisticDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new TournamentResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await _matchesStatisticService.CreateMatchesStatistic(createMatchesStatisticDto);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new TournamentResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new TournamentResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpPut("updateMatchesStatistic")]
        public async Task<IActionResult> UpdateMatchesStatistic(UpdateMatchesStatisticDto updateMatchesStatisticDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new TournamentResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await _matchesStatisticService.UpdateMatchesStatistic(updateMatchesStatisticDto);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new TournamentResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new TournamentResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpDelete("deleteMatchesStatistic/{MatchesStatisticID}")]
        public async Task<IActionResult> DeleteMatchesStatistic(int MatchesStatisticID)
        {
            try
            {
                var result = await _matchesStatisticService.DeleteMatchesStatistic(MatchesStatisticID);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new TournamentResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new TournamentResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpGet("getMatchesStatistic/{MatchesStatisticID}")]
        public async Task<IActionResult> GetMatchesStatistic(int MatchesStatisticID)
        {
            try
            {
                var result = await _matchesStatisticService.GetMatchesStatistic(MatchesStatisticID);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new TournamentResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new TournamentResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpGet("getMatchesStatistics")]
        public async Task<IActionResult> GetMatchesStatistics()
        {
            try
            {
                var result = await _matchesStatisticService.GetMatchesStatistics();
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new TournamentResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new TournamentResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
    }
}
