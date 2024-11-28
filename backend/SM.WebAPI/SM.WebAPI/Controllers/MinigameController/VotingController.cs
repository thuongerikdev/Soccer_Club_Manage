using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.Minigame.Abtracts.Vote;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.MinigameDto.Vote;

namespace SM.WebAPI.Controllers.MinigameController
{
    [Route("api/minigame")]
    [ApiController]
    public class VotingController : Controller
    {
        private readonly IVoteService _votingService;
        public VotingController(IVoteService votingService)
        {
            _votingService = votingService;
        }

        //[HttpGet("getListPlayerVote/{minigameID}")]
        //public async Task<IActionResult> GetListPlayerVote(int minigameID)
        //{
        //    try
        //    {
        //        var result = await _votingService.GetListPlayerVote(minigameID);
        //        if (result.ErrorCode != 0)
        //        {
        //            return BadRequest(new TournamentResponeDto
        //            {
        //                ErrorMessage = result.ErrorMessage,
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
        //        return BadRequest(new TournamentResponeDto
        //        {
        //            ErrorCode = 1,
        //            ErrorMessage = ex.Message,
        //            Data = null
        //        });
        //    }
        //}


        [HttpPost("createVote")]
        public async Task<IActionResult> CreateVote(CreateVoteDto createVoteDto)
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
                var result = await _votingService.CreateVote(createVoteDto);
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
        [HttpPut("updateVote")]
        public async Task<IActionResult> UpdateVote(UpdateVoteDto updateVoteDto)
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
                var result = await _votingService.UpdateVote(updateVoteDto);
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
        [HttpDelete("deleteVote/{voteID}")]
        public async Task<IActionResult> DeleteVote(int voteID)
        {
            try
            {
                var result = await _votingService.DeleteVote(voteID);
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
        [HttpGet("getVote/{voteID}")]
        public async Task<IActionResult> GetVote(int voteID)
        {
            try
            {
                var result = await _votingService.GetVotebyID(voteID);
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
        [HttpGet("getVoteByUser/{minigameID}")]
        public async Task<IActionResult> GetVoteByUser(int minigameID)
        {
            try
            {
                var result = await _votingService.GetVotesByMinigame(minigameID);
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
        [HttpGet("getallvote")]
        public async Task<IActionResult> GetAllVote()
        {
            try
            {
                var result = await _votingService.GetAllVotes();
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
        [HttpGet("GetListPlayerVote/{minigameID}")]
        public async Task<IActionResult> GetListPlayerVotes(int minigameID)
        {
            try
            {
                var result = await _votingService.ListPlayerCanVote(minigameID);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorMessage = result.ErrorMessage,
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
