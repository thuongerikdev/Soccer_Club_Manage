using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.NotificationModule.Abtracts;
using SM.Tournament.Dtos;

namespace SM.WebAPI.Controllers.NotificationController
{
    [Route("notification")]
    [ApiController]
    public class NotiController : Controller
    {
        private readonly IEmailService _emailService;
        public NotiController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpGet("sendEmail")]
        public async Task<IActionResult> SendEmailTest( int to)
        {
            try
            {
                var respone = await _emailService.SendInvoiceEmail(to);
                return Ok(new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Send email success",
                    Data = respone.Data
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
