using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.TournamentDto.TournamentClub;

namespace SM.WebAPI.Controllers.TournamentController
{
    [Route("tournament")]
    [ApiController]
    public class TourClubController : Controller
    {
        private readonly ITournamentClubService _tournamentClubService;
        public TourClubController(ITournamentClubService tournamentClubService)
        {
            _tournamentClubService = tournamentClubService;
        }
        [HttpPost("CreateTournamentClub")]
        public async Task<IActionResult> CreateTournamentClub(CreateTournamentClubDto createTournamentClubDto)
        {
            try
            {
                var result = await _tournamentClubService.createTournamentClub(createTournamentClubDto);
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
        [HttpPut("UpdateTournamentClub")]
        public async Task<IActionResult> UpdateTournamentClub(UpdateTournamentClubDto updateTournamentClubDto)
        {
            try
            {
                var result = await _tournamentClubService.updateTournamentClub(updateTournamentClubDto);
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
        [HttpDelete("DeleteTournamentClub/{TournamentClubID}")]
        public async Task<IActionResult> DeleteTournamentClub(int tournamentClubID)
        {
            try
            {
                var result = await _tournamentClubService.deleteTournamentClub(tournamentClubID);
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
        [HttpGet("GetTournamentClub/{TournamentClubID}")]
        public async Task<IActionResult> GetTournamentClub(int tournamentClubID)
        {
            try
            {
                var result = await _tournamentClubService.getTournamentClub(tournamentClubID);
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
        [HttpGet("GetTournamentClubs")]
        public async Task<IActionResult> GetTournamentClubs()
        {
            try
            {
                var result = await _tournamentClubService.getTournamentClubs();
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
