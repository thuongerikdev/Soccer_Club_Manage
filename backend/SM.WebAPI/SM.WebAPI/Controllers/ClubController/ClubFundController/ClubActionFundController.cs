using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.TournamentModule.ClubModule.Abtracts.ClubFund;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubFund.ActionFundHistory;
using SM.Tournament.Dtos.PlayerDto.PlayerFund;

namespace SM.WebAPI.Controllers.ClubController.ClubFundController
{
    [Route("api/ClubFund")]
    [ApiController]
    public class ClubActionFundController : Controller
    {
        private readonly ICaculateService _caculateService;
        public ClubActionFundController(ICaculateService caculateService)
        {
            _caculateService = caculateService;
        }
        [HttpPost("caculateFund/{createActionFundDto.FundActionType}")]
        public async Task<IActionResult> AddContribution(CreateActionFundDto createActionFundDto)
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
                var result = await _caculateService.CaculateFunds(createActionFundDto);
                if (result.ErrorCode != 0)
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
        //[HttpPost("addSpent")]
        //public async Task<IActionResult> AddSpent(CreateActionFundDto createActionFundDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new TournamentResponeDto
        //        {
        //            ErrorMessage = "Invalid input data.",
        //            ErrorCode = 1,
        //            Data = ModelState
        //        });
        //    }

        //    try
        //    {
        //        var result = await _caculateService.AddSpent(createActionFundDto);
        //        if (result.ErrorCode != 0)
        //        {
        //            return BadRequest(new TournamentResponeDto
        //            {
        //                ErrorMessage = "Invalid credentials.",
        //                ErrorCode = 1,
        //                Data = null
        //            });
        //        }

        //        return Ok(new TournamentResponeDto
        //        {
        //            ErrorMessage = result.ErrorMessage,
        //            ErrorCode = result.ErrorCode,
        //            Data = result.Data
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new TournamentResponeDto
        //        {
        //            ErrorMessage = "Internal server error: " + ex.Message,
        //            ErrorCode = 1,
        //            Data = null
        //        });
        //    }
        //}
        //[HttpPost("addDept")]
        //public async Task<IActionResult> AddDept(CreateActionFundDto createActionFundDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new TournamentResponeDto
        //        {
        //            ErrorMessage = "Invalid input data.",
        //            ErrorCode = 1,
        //            Data = ModelState
        //        });
        //    }

        //    try
        //    {
        //        var result = await _caculateService.AddDept(createActionFundDto );
        //        if (result.ErrorCode != 0)
        //        {
        //            return BadRequest(new TournamentResponeDto
        //            {
        //                ErrorMessage = "Invalid credentials.",
        //                ErrorCode = 1,
        //                Data = null
        //            });
        //        }

        //        return Ok(new TournamentResponeDto
        //        {
        //            ErrorMessage = result.ErrorMessage,
        //            ErrorCode = result.ErrorCode,
        //            Data = result.Data
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new TournamentResponeDto
        //        {
        //            ErrorMessage = "Internal server error: " + ex.Message,
        //            ErrorCode = 1,
        //            Data = null
        //        });
        //    }
        //}

    }
}
