using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.Minigame.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Reward;

namespace SM.WebAPI.Controllers.MinigameController
{
    [Route("api/rewards")]
    [ApiController]
    public class MinigameRewardController : Controller
    {
        private readonly IRewardService _minigameRewardService;
        public MinigameRewardController(IRewardService minigameRewardService)
        {
            _minigameRewardService = minigameRewardService;
        }
        [HttpPost("CreateReward")]
        public async Task<IActionResult> CreateReward(CreateRewardDto createRewardDto)
        {
            try
            {
                var result = await _minigameRewardService.createReward(createRewardDto);
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
        [HttpDelete("DeleteReward/{rewardID}")]
        public async Task<IActionResult> DeleteReward(int rewardID)
        {
            try
            {
                var result = await _minigameRewardService.deleteReward(rewardID);
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
        [HttpGet("GetReward/{rewardID}")]
        public async Task<IActionResult> GetReward(int rewardID)
        {
            try
            {
                var result = await _minigameRewardService.getReward(rewardID);
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
        [HttpGet("GetRewards")]
        public async Task<IActionResult> GetRewards()
        {
            try
            {
                var result = await _minigameRewardService.getRewards();
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
        [HttpPut("UpdateReward")]
        public async Task<IActionResult> UpdateReward(UpdateRewardDto updateRewardDto)
        {
            try
            {
                var result = await _minigameRewardService.updateReward(updateRewardDto);
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
