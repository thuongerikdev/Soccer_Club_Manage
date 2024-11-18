using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.PlayerModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerLineUp;

namespace SM.WebAPI.Controllers.PlayerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerLineUpController : Controller
    {
        private readonly IPlayerLineUpService _playerLineUpService;
        public PlayerLineUpController (IPlayerLineUpService playerLineUpService)
        {
            _playerLineUpService = playerLineUpService;
        }
        [HttpPost("CreatePlayerLineUp")]
        public async Task<IActionResult> CreatePlayerLineUp(List<CreatePlayerLineUpDto> createPlayerLineUpDtos )
        {
            try
            {
                var result = await _playerLineUpService.CreatePlayerLineUps(createPlayerLineUpDtos);
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage,
                    Data = result.Data
                });
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
        [HttpPut("UpdatePlayerLineUp")]
        public async Task<IActionResult> UpdatePlayerLineUp(UpdatePlayerLineUpDto updatePlayerLineUpDto)
        {
            try
            {
                var result = await _playerLineUpService.UpdatePlayerLineUp(updatePlayerLineUpDto);
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage,
                    Data = result.Data
                });
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
        [HttpDelete("DeletePlayerLineUp")]
        public async Task<IActionResult> DeletePlayerLineUp(int PlayerLineUpID)
        {
            try
            {
                var result = await _playerLineUpService.DeletePlayerLineUp(PlayerLineUpID);
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage,
                    Data = result.Data
                });
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
        [HttpGet("GetPlayerLineUp")]
        public async Task<IActionResult> GetPlayerLineUp()
        {
            try
            {
                var result = await _playerLineUpService.GetPlayerLineUp();
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage,
                    Data = result.Data
                });
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
        [HttpGet("getPlayerLineUpByLineUP/{LineUpID}")]
        public async Task<IActionResult> getPlayerLineUpByLineUP(int LineUpID)
        {
            try
            {
                var result = await _playerLineUpService.GetPlayerLineUpByLineUp(LineUpID);
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage,
                    Data = result.Data
                });
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
