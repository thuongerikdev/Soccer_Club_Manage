using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.OrderDto.OrderModel.Invoice;

namespace SM.WebAPI.Controllers.OrderController
{
    [Route("invoice")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceDto createInvoiceDto)
        {
            try
            {
                var result = await _invoiceService.CreateInvoice(createInvoiceDto);
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
        [HttpPut("update")]
        public async Task<IActionResult> UpdateInvoice(UpdateInvoiceDto updateInvoiceDto)
        {
            try
            {
                var result = await _invoiceService.UpdateInvoice(updateInvoiceDto);
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
        [HttpDelete("delete/{invoiceID}")]
        public async Task<IActionResult> DeleteInvoice(int invoiceID)
        {
            try
            {
                var result = await _invoiceService.DeleteInvoice(invoiceID);
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
        [HttpGet("get/{invoiceID}")]
        public async Task<IActionResult> GetInvoice(int invoiceID)
        {
            try
            {
                var result = await _invoiceService.GetInvoice(invoiceID);
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
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllInvoice()
        {
            try
            {
                var result = await _invoiceService.GetInvoices();
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
