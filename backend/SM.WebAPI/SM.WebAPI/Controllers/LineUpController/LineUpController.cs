using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.LineUpModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.LineUpDto.LineUp;

namespace SM.WebAPI.Controllers.LineUpController
{
    [Route("api/LineUp")]
    [ApiController]
    public class LineUpController : Controller
    {
        private readonly ILineUpService _lineUpService;
        public LineUpController(ILineUpService lineUpService)
        {
            _lineUpService = lineUpService;
        }
        [HttpPost("createLineUp")]
        public async Task<IActionResult> CreateLineUp(CreateLineUpDto createLineUpDto)
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
                var result = await _lineUpService.CreateLineUp(createLineUpDto);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
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
        [HttpPut("updateLineUp")]
        public async Task<IActionResult> UpdateLineUp(UpdateLineUpDto updateLineUpDto)
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
                var result = await _lineUpService.UpdateLineUp(updateLineUpDto);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
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
        [HttpDelete("deleteLineUp")]
        public async Task<IActionResult> DeleteLineUp(int lineUpID)
        {
            try
            {
                var result = await _lineUpService.DeleteLineUp(lineUpID);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
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
        [HttpGet("getLineUpById")]
        public async Task<IActionResult> GetLineUpById(int lineUpID)
        {
            try
            {
                var result = await _lineUpService.GetLineUpById(lineUpID);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
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
        [HttpGet("getAllLineUp")]
        public async Task<IActionResult> GetAllLineUp()
        {
            try
            {
                var result = await _lineUpService.GetAllLineUp();
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
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
