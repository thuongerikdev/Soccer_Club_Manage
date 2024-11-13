using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.ClubModule.Abtracts.ClubEvents.Statistic;
using SM.Tournament.ApplicationService.ClubModule.Implements.ClubEvents;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.PlayerDto.PlayerEvent;

namespace SM.WebAPI.Controllers.ClubController.ClubEventController
{
    [Route("api/eventStatistic")]
    [ApiController]
    public class EventStatisticController : Controller
    {
        private readonly EventFactorySerivce _eventFactorySerivce;
        public EventStatisticController(EventFactorySerivce eventFactorySerivce)
        {
            _eventFactorySerivce = eventFactorySerivce;
        }
        [HttpGet ("playerEventStatistic/{type}")]
        public async Task<IActionResult> EventStatistic(string type ,[FromQuery] ReadPlayerEventDto readPlayerEventDto)
        {
            try
            {
                if (readPlayerEventDto == null)
                {
                    // Xử lý khi không có dữ liệu từ query
                    return BadRequest(new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Invalid request: ReadPlayerEventDto is null.",
                        Data = null
                    });
                }
                var service = _eventFactorySerivce.eventStatisticStrategy(type);
                var result = await ((IEventStatisticStrategy)service).EventStatistic(readPlayerEventDto);
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
                        ErrorMessage = "Event Statistic Success",
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
    }
}
