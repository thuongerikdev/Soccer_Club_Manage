using Microsoft.AspNetCore.Mvc;
using SM.LineUp.ApplicationService.Module.Abtracts;
using SM.LineUp.Dtos;
using SM.LineUp.Dtos.PlayerLineUp;
using SM.Player.Dtos.PlayerDto;

namespace SM.WebAPI.Controllers.LineUpController
{
    [Route("api/playerLineup")]
    [ApiController]
    public class PlayerLineUpController : Controller
    {
        private readonly IPlayerLineUp _playerLineUpService;
        public PlayerLineUpController(IPlayerLineUp playerLineUpService)
        {
            _playerLineUpService = playerLineUpService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayerLineUps(List<PlayerLineUpDto> createPlayerLineUpDtos)
        {
            if (createPlayerLineUpDtos == null || !createPlayerLineUpDtos.Any())
            {
                return BadRequest(new LineUpResponeDto
                {
                    ErrorMessage = "Input data cannot be null or empty.",
                    ErrorCode = 1,
                    Data = null
                });
            }

            foreach (var dto in createPlayerLineUpDtos)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new LineUpResponeDto
                    {
                        ErrorMessage = "Invalid input data for one or more line-ups.",
                        ErrorCode = 1,
                        Data = ModelState
                    });
                }
            }

            try
            {
                var result = await _playerLineUpService.CreatePlayerLineUps(createPlayerLineUpDtos);
                if (result == null)
                {
                    return BadRequest(new LineUpResponeDto
                    {
                        ErrorMessage = "Failed to create player line-ups.",
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
        [HttpPut("update/{updatePlayerLineUpDto.PlayerLineUpId}")]
        public async Task<IActionResult> UpdatePlayerLineUp(UpdatePlayerLineUpDto updatePlayerLineUpDto)
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
                var result = await _playerLineUpService.UpdatePlayerLineUp(updatePlayerLineUpDto);
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
        [HttpDelete("delete/{playerLineUpId}")]
        public async Task<IActionResult> RemovePlayerLineUp(int playerLineUpId)
        {
            try
            {
                var result = await _playerLineUpService.RemovePlayerLineUp(playerLineUpId);
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
        public async Task<IActionResult> GetAllPlayerLineUp()
        {
            try
            {
                var result = await _playerLineUpService.GetAllPlayerLineUp();
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
        [HttpGet("get/{playerLineUpId}")]
        public async Task<IActionResult> GetPlayerLineUp(int playerLineUpId)
        {
            try
            {
                var result = await _playerLineUpService.GetPlayerLineUpById(playerLineUpId);
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
