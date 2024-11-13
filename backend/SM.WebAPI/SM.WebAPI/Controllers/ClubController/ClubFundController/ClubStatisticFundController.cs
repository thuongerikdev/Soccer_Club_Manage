using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubFund.FundStatistic;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubFund.Statistic;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;

namespace SM.WebAPI.Controllers.ClubController.ClubFundController
{
    [Route("api/ClubFund")]
    [ApiController]
    public class ClubStatisticFundController : Controller
    {
        private IFundStatisticService _fundStatisticService;
        public ClubStatisticFundController(IFundStatisticService fundStatisticService)
        {
            _fundStatisticService = fundStatisticService;
        }

        [HttpGet("FundStatistic/{strategyType}")]
        public async Task<IActionResult> FundStatistic(string strategyType, [FromQuery] ReadActionFundDto readActionFundDto)
        {
           try
            {
                var result = await _fundStatisticService.FundStatistic(strategyType, readActionFundDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
