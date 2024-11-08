using Microsoft.AspNetCore.Mvc;
using SM.Match.ApplicationService.Module.MatchesModule.Abtracts;
using SM.Match.Dtos;
using SM.Match.Dtos.MatchesDto.Matches;

namespace SM.WebAPI.Controllers.MatchesController
{
    [Route("api/matches")]
    [ApiController]
    public class MatchesController : Controller
    {
        private readonly IMatchesService _matchesService;
        public MatchesController(IMatchesService matchesService)
        {
            _matchesService = matchesService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMatches(CreateMatchesDto createMatchesDto)
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
                var result = await _matchesService.CreateMatches(createMatchesDto);
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
        [HttpPut("update/{updateMatchesDto.MatchesId}")]
        public async Task<IActionResult> UpdateMatches(UpdateMatchesDto updateMatchesDto)
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
                var result = await _matchesService.UpdateMatches(updateMatchesDto);
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
        [HttpDelete("remove/{matchesId}")]
        public async Task<IActionResult> RemoveMatches(int matchesId)
        {
            try
            {
                var result = await _matchesService.RemoveMatches(matchesId);
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
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllMatches()
        {
            try
            {
                var result = await _matchesService.GetAllMatches();
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
        [HttpGet("get/{matchesId}")]
        public async Task<IActionResult> GetMatches(int matchesId)
        {
            try
            {
                var result = await _matchesService.GetMatchesById(matchesId);
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
    }
}
