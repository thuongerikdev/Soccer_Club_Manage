using Microsoft.AspNetCore.Mvc;
using SM.Match.ApplicationService.Module.MatchesModule.Abtracts;
using SM.Match.Dtos;
using SM.Match.Dtos.MatchesDto.MatchesStatistic;

namespace SM.WebAPI.Controllers.MatchesController
{
    [Route("api/matchesStatistic")]
    [ApiController]
    public class MatchesStatisticController : Controller
    {
        private readonly IMatchesStatisticService matchesStatisticService;
        public MatchesStatisticController(IMatchesStatisticService matchesStatisticService)
        {
            this.matchesStatisticService = matchesStatisticService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateMatchesStatistic(CreateMatchesStatisticDto createMatchesStatisticDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MatchResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await matchesStatisticService.CreateMatchStatistic(createMatchesStatisticDto);
                if (result == null)
                {
                    return BadRequest(new MatchResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new MatchResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MatchResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpPut("update/{updateMatchesStatisticDto.MatchesStatisticId}")]
        public async Task<IActionResult> UpdateMatchesStatistic(UpdateMatchesStatisticDto updateMatchesStatisticDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new MatchResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await matchesStatisticService.UpdateMatchStatistic(updateMatchesStatisticDto);
                if (result == null)
                {
                    return BadRequest(new MatchResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new MatchResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MatchResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpDelete("remove/{matchesStatisticId}")]
        public async Task<IActionResult> RemoveMatchesStatistic(int matchesStatisticId)
        {
            try
            {
                var result = await matchesStatisticService.RemoveMatchStatistic(matchesStatisticId);
                if (result == null)
                {
                    return BadRequest(new MatchResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new MatchResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MatchResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllMatchesStatistic()
        {
            try
            {
                var result = await matchesStatisticService.GetAllMatchStatistic();
                if (result == null)
                {
                    return BadRequest(new MatchResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new MatchResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MatchResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpGet("getById/{matchesStatisticId}")]
        public async Task<IActionResult> GetMatchesStatisticById(int matchesStatisticId)
        {
            try
            {
                var data = await matchesStatisticService.GetMatchStatisticById(matchesStatisticId);
                if (data == null)
                {
                    return BadRequest(new MatchResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }
                return Ok(new MatchResponeDto
                {
                    ErrorMessage = data.ErrorMessage,
                    ErrorCode = data.ErrorCode,
                    Data = data.Data
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new MatchResponeDto
                {
                    ErrorMessage = "Internal server error: " + e.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
    }

}
