using Microsoft.AspNetCore.Mvc;
using SM.LineUp.ApplicationService.Module.Abtracts;
using SM.LineUp.Dtos;
using SM.LineUp.Dtos.LineUpDtos;

namespace SM.WebAPI.Controllers.LineUpController
{
    [Route("api/lineup")]
    [ApiController]
    public class LineUpController : Controller
    {
        protected ILineUpService LineUpService;
        public LineUpController(ILineUpService lineUpService)
        {
            LineUpService = lineUpService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateLineUp(CreateLineUpDto createLineUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LineUpResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await LineUpService.CreateLineUp(createLineUpDto);
                if (result.ErrorCode!=0)
                {
                    return BadRequest(new LineUpResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new LineUpResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LineUpResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateLineUp(UpdateLineUpDto updateLineUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LineUpResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await LineUpService.UpdateLineUp(updateLineUpDto);
                if (result == null)
                {
                    return BadRequest(new LineUpResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new LineUpResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LineUpResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpDelete("remove/{lineUpId}")]
        public async Task<IActionResult> RemoveLineUp(int lineUpId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LineUpResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await LineUpService.RemoveLineUp(lineUpId);
                if (result == null)
                {
                    return BadRequest(new LineUpResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new LineUpResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LineUpResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllLineUp()
        {
            try
            {
                var result = await LineUpService.GetAllLineUp();
                if (result == null)
                {
                    return BadRequest(new LineUpResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new LineUpResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LineUpResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }
        [HttpGet("get/{lineUpId}")]
        public async Task<IActionResult> GetLineUpById(int lineUpId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LineUpResponeDto
                {
                    ErrorMessage = "Invalid input data.",
                    ErrorCode = 1,
                    Data = ModelState
                });
            }

            try
            {
                var result = await LineUpService.GetLineUpById(lineUpId);
                if (result == null)
                {
                    return BadRequest(new LineUpResponeDto
                    {
                        ErrorMessage = "Invalid credentials.",
                        ErrorCode = 1,
                        Data = null
                    });
                }

                return Ok(new LineUpResponeDto
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LineUpResponeDto
                {
                    ErrorMessage = "Internal server error: " + ex.Message,
                    ErrorCode = 1,
                    Data = null
                });
            }
        }

    }
}