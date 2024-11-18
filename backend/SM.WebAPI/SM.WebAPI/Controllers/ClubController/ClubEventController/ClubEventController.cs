using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents.Statistic;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.ClubDto.ClubEvent.ClubEvent;

namespace SM.WebAPI.Controllers.ClubController.ClubEventController
{
    [Route("api/clubevents")]
    [ApiController]
    public class ClubEventController : Controller
    {
        private readonly IEventStrategyUse _clubEventStrategyUse;
        public ClubEventController(IEventStrategyUse clubEventStrategyUse)
        {
           _clubEventStrategyUse = clubEventStrategyUse;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            try
            {
                var service = _clubEventStrategyUse.CreateEventsService(createEventDto.EventType);
                var result = await service.CreateEvent(createEventDto);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = result.ErrorMessage,
                        Data = null
                    });
                }
                else
                {
                    return Ok(new TournamentResponeDto
                    {
                        ErrorCode = 0,
                        ErrorMessage = "Create Event Success",
                        Data = result.Data
                    });

                }
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
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEvent(UpdateEventDto updateEventDto)
        {
            try
            {
                var service = _clubEventStrategyUse.CreateEventsService(updateEventDto.EventType);
                var result = await service.UpdateEvent(updateEventDto);

                if (result.ErrorCode != 0) {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = result.ErrorMessage,
                        Data = null
                    });
                }
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Event Success",
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
        [HttpDelete("delete/{EventID}")]
        public async Task<IActionResult> DeleteEvent(int EventID, string EventType)
        {
            try
            {
                var service = _clubEventStrategyUse.CreateEventsService(EventType);
                var result = await service.RemoveEvent(EventID);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = result.ErrorMessage,
                        Data = null
                    });
                }
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Event Success",
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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllEvent(string EventType)
        {
            try {
                var service = _clubEventStrategyUse.CreateEventsService(EventType);
                var result = await service.GetAllEvents();
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = result.ErrorMessage,
                        Data = null
                    });
                }
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get All Event Success",
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
        [HttpGet("getbyId/{EventID}")]
        public async Task<IActionResult> GetEventByID(int EventID, string EventType)
        {
            try
            {
                var service = _clubEventStrategyUse.CreateEventsService(EventType);
                var result = await service.GetEventById(EventID);
                if (result.ErrorCode != 0)
                {
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = result.ErrorCode,
                        ErrorMessage = result.ErrorMessage,
                        Data = null
                    });
                }
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Get Event Success",
                    Data = result.Data
                });

            }
            catch (Exception ex) { 
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

