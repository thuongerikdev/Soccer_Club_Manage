using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.TournamentDto.Tournament;

namespace SM.WebAPI.Controllers.TournamentController
{
    [Route("tournament")]
    [ApiController]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;
        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }
        [HttpPost("CreateTournament")]
        public async Task<IActionResult> CreateTournament(CreateTournamentDto createTournamentDto)
        {
            try
            {
                var result = await _tournamentService.createTournament(createTournamentDto);
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
        [HttpPut("UpdateTournament")]
        public async Task<IActionResult> UpdateTournament(UpdateTournamentDto updateTournamentDto)
        {
            try
            {
                var result = await _tournamentService.updateTournament(updateTournamentDto);
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
        [HttpDelete("DeleteTournament/{TournamentID}")]
        public async Task<IActionResult> DeleteTournament(int tournamentID)
        {
            try
            {
                var result = await _tournamentService.deleteTournament(tournamentID);
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
        [HttpGet("GetTournament/{TournamentID}")]
        public async Task<IActionResult> GetTournament(int tournamentID)
        {
            try
            {
                var result = await _tournamentService.getTournament(tournamentID);
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
        [HttpGet("GetAllTournament")]
        public async Task<IActionResult> GetAllTournament()
        {
            try
            {
                var result = await _tournamentService.getTournaments();
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
