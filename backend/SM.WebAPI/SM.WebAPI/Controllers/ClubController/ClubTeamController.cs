using Microsoft.AspNetCore.Mvc;
using SM.Club.ApplicationService.Module.ClubModule.Abtracts;
using SM.Club.Dtos;
using SM.Club.Dtos.ClubTeamDtos;

namespace SM.WebAPI.Controllers.ClubController
{
    [Route("api/clubs")]
    [ApiController]
    public class ClubTeamController : Controller
    {

        public readonly IClubTeamService _clubTeamService;

        public ClubTeamController(IClubTeamService clubService)
        {
            _clubTeamService = clubService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateClubTeam(CreateClubTeamDto createClubTeamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ClubResponeDto
                {
                    EM = "Invalid input data.",
                    EC = 1,
                    DT = ModelState
                });
            }

            try
            {
                var result = await _clubTeamService.CreateClubTeam(createClubTeamDto);
                if (result == null)
                {
                    return BadRequest(new ClubResponeDto
                    {
                        EM = "Invalid credentials.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(new ClubResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ClubResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
        [HttpPut("update/{updateClubTeamDto.ClubId}")]
        public async Task<IActionResult> UpdateClubTeam(UpdateClubTeamDto updateClubTeamDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ClubResponeDto
                {
                    EM = "Invalid input data.",
                    EC = 1,
                    DT = ModelState
                });
            }

            try
            {
                var result = await _clubTeamService.UpdateClubTeam(updateClubTeamDto);
                if (result == null)
                {
                    return BadRequest(new ClubResponeDto
                    {
                        EM = "Invalid credentials.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(new ClubResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ClubResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
        [HttpDelete("remove/{clubTeamId}")]
        public async Task<IActionResult> RemoveClubTeam(int clubTeamId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ClubResponeDto
                {
                    EM = "Invalid input data.",
                    EC = 1,
                    DT = ModelState
                });
            }

            try
            {
                var result = await _clubTeamService.RemoveClubTeam(clubTeamId);
                if (result == null)
                {
                    return BadRequest(new ClubResponeDto
                    {
                        EM = "Invalid credentials.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(new ClubResponeDto
                {
                    EM = result.EM,
                    EC = result.EC,
                    DT = result.DT
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ClubResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllClubTeam()
        {
            try
            {
                var result = await _clubTeamService.GetAllClubTeam();
                return Ok(result); // Return 200 OK with user list
            }
            catch (Exception ex)
            {
                // Log the exception (if needed)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get/{clubTeamId}")]
        public async Task<IActionResult> GetClubTeamById(int clubTeamId)
        {
            try
            {
                var result = await _clubTeamService.GetClubTeamById(clubTeamId);
                if (result == null)
                {
                    return NotFound(new ClubResponeDto
                    {
                        EM = "Tournament not found.",
                        EC = 1,
                        DT = null
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ClubResponeDto
                {
                    EM = "Internal server error: " + ex.Message,
                    EC = 1,
                    DT = null
                });
            }
        }
    }
}
