using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using System.Threading.Tasks;

namespace SM.WebAPI.Controllers.TournamentController
{
    [Route("api/TourClub")]
    [ApiController]
    public class TournamentMatchesController : Controller
    {
        private readonly ITourMatchStrategy _tourMatchStrategy;

        public TournamentMatchesController(ITourMatchStrategy tourMatchStrategy)
        {
            _tourMatchStrategy = tourMatchStrategy;
        }

        [HttpPost("CreateTournamentMatch/{tournamentID}")]
        public async Task<IActionResult> CreateTournamentMatch(int tournamentID)
        {
            var result = await _tourMatchStrategy.CreateTournamentMatch(tournamentID);
            return Ok(result);
        }

        [HttpPut("ProcessMatchResult/{matchID}")]
        public async Task<IActionResult> ProcessMatchResult(int matchID)
        {
            var result = await _tourMatchStrategy.ProcessMatchResult(matchID);
            return Ok(result);
        }

        [HttpGet("GetStandings/{TournamentID}")]
        public async Task<IActionResult> GetStandings(int TournamentID)
        {
            var result = await _tourMatchStrategy.GetStandings(TournamentID);
            return Ok(result);
        }

        [HttpPost("CreateSemiFinalMatches/{TournamentID}")]
        public async Task<IActionResult> CreateSemiFinalMatches(int TournamentID)
        {
            var result = await _tourMatchStrategy.CreateSemiFinalMatches(TournamentID);
            return Ok(result);
        }

        [HttpPost("CreateFinalAndThirdPlaceMatches/{TournamentID}")]
        public async Task<IActionResult> CreateFinalAndThirdPlaceMatches(int TournamentID)
        {
            var result = await _tourMatchStrategy.CreateFinalAndThirdPlaceMatches(TournamentID);
            return Ok(result);
        }

        [HttpGet("DetermineFinalRankings/{TournamentID}")]
        public async Task<IActionResult> DetermineFinalRankings(int TournamentID)
        {
            var result = await _tourMatchStrategy.DetermineFinalRankings(TournamentID);
            return Ok(result);
        }
    }
}
