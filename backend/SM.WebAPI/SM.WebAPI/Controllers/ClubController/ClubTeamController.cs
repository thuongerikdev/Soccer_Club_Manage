using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.ClubModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.Club;

namespace SM.WebAPI.Controllers.ClubController
{
    [Route("api/clubTeam")]
    [ApiController]
    public class ClubTeamController : Controller
    {
        private readonly IClubService clubTeamService;
        public ClubTeamController(IClubService clubTeamService)
        {
            this.clubTeamService = clubTeamService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateClub(CreateClubDto createClubDto)
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
                var result = await clubTeamService.CreateClubTeam(createClubDto);
                if (result == null)
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
        [HttpPut("update/{updateClubDto.ClubID}")]
        public async Task<IActionResult> UpdateClub(UpdateClubDto updateClubDto)
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
                var result = await clubTeamService.UpdateClubTeam(updateClubDto);
                if (result == null)
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
        [HttpDelete("delete/{clubID}")]
        public async Task<IActionResult> DeleteClub(int clubID)
        {
            try
            {
                var result = await clubTeamService.RemoveClubTeam(clubID);
                if (result == null)
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
        [HttpGet("get/{clubID}")]
        public async Task<IActionResult> GetClub(int clubID)
        {
            try
            {
                var result = await clubTeamService.GetClubTeamById(clubID);
                if (result == null)
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
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllClub()
        {
            try
            {
                var result = await clubTeamService.GetAllClubTeam();
                if (result == null)
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
        [HttpGet("getyourClub/{userID}")]
        public async Task<IActionResult> GetYourClub(int userID)
        {
            try
            {
                var result = await clubTeamService.GetClubTeamByUserId(userID);
                if (result == null)
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
        [HttpGet("findbyName/{name}")]
        public async Task<IActionResult> FindClubByName(string name)
        {
            try
            {
                var result = await clubTeamService.FindClubByName(name);
                if (result == null)
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
