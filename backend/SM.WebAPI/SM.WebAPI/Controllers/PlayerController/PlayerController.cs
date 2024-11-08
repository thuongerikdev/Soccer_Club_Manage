using Microsoft.AspNetCore.Mvc;
using SM.Auth.Dtos;
using SM.Player.ApplicationService.Module.PlayerModule.Abtracts;
using SM.Player.Dtos;
using SM.Player.Dtos.PlayerDto;

namespace SM.WebAPI.Controllers.PlayerController
{
    [Route("api/players")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new PlayerResponeDto
                {
                    EM = "Invalid input data.",
                    EC = 1,
                    DT = ModelState
                });
            }

            try
            {
                var result = await _playerService.CreatePlayer(createPlayerDto);
                if (result == null)
                {
                    return BadRequest(new PlayerResponeDto
                    {
                        EM = "Invalid credentials.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(new PlayerResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PlayerResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
        [HttpPut("update/{playerId}")]
        public async Task<IActionResult> UpdatePlayer(int playerId , UpdatePlayerDto updatePlayerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new PlayerResponeDto
                {
                    EM = "Invalid input data.",
                    EC = 1,
                    DT = ModelState
                });
            }

            try
            {
                var result = await _playerService.UpdatePlayer(playerId ,updatePlayerDto);
                if (result == null)
                {
                    return BadRequest(new PlayerResponeDto
                    {
                        EM = "Invalid credentials.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(new PlayerResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PlayerResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
        [HttpDelete("delete/{playerId}")]
        public async Task<IActionResult> DeletePlayer(int playerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new PlayerResponeDto
                {
                    EM = "Invalid input data.",
                    EC = 1,
                    DT = ModelState
                });
            }

            try
            {
                var result = await _playerService.DeletePlayer(playerId);
                if (result.EC != 0)
                {
                    return BadRequest(new PlayerResponeDto
                    {
                        EM = result.EM,
                        EC = result.EC,
                        DT = result.DT
                    });
                }

                return Ok(new PlayerResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PlayerResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPlayer()
        {
            try
            {
                var result = await _playerService.GetAllPlayer();
                if (result == null)
                {
                    return BadRequest(new PlayerResponeDto
                    {
                        EM = "Invalid credentials.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(new PlayerResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PlayerResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
        [HttpGet("getById/{PlayerId}")]
        public async  ValueTask<PlayerResponeDto> GetPlayerById(int PlayerId)
        {
            var result = await _playerService.GetPlayerById(PlayerId);

            if(result.EC != 0)
            {
                return new PlayerResponeDto
                {
                    EC = result.EC,
                    EM = result.EM,
                    DT = result.DT
                };
            }
            return new PlayerResponeDto
            {
                EC = result.EC,
                EM = result.EM,
                DT = result.DT
            };
        }


    }
}
