using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.OrderDto;

namespace SM.WebAPI.Controllers.OrderController
{
    [Route("Order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var result = await _orderService.CreateOrder(createOrderDto);
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
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            try
            {
                var result = await _orderService.UpdateOrder(updateOrderDto);
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
        [HttpDelete("deleteOrder")]
        public async Task<IActionResult> DeleteOrder(int orderID)
        {
            try
            {
                var result = await _orderService.DeleteOrder(orderID);
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
        [HttpGet("getByID/{orderID}")]
        public async Task<IActionResult> GetOrder(int orderID)
        {
            try
            {
                var result = await _orderService.GetOrder(orderID);
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
        [HttpGet("getAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var result = await _orderService.GetOrders();
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
