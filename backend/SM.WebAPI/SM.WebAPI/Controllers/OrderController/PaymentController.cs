using Microsoft.AspNetCore.Mvc;
using SM.Tournament.ApplicationService.NotificationModule.Abtracts;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Domain.Orders;
using SM.Tournament.Dtos.OrderDto;
using SM.Tournament.Dtos.OrderDto.OrderModel.Invoice;
using SM.Tournament.Dtos.OrderDto.OrderModel.Momo;
using SM.Tournament.Dtos.OrderDto.OrderModel.VnPay;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : Controller
{
    private readonly IMomoService _momoService;
    private readonly IVnPayService _vnPayService;
    private readonly IOrderService _orderService;
    private readonly IInvoiceService _invoiceService;
    private readonly IEmailService _emailService;


    public PaymentController(IMomoService momoService, IVnPayService vnPayService , IOrderService orderService, IInvoiceService invoiceService , IEmailService emailService)
    {
        _momoService = momoService;
        _vnPayService = vnPayService;
        _orderService = orderService;
        _invoiceService = invoiceService;
        _emailService = emailService;
    }

    [HttpPost("momo/payment")]
    public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model)
    {
        var response = await _momoService.CreatePaymentAsync(model);
        return Ok(new { PayUrl = response.PayUrl });
    }

    [HttpGet("momo/PaymentExecute")]
    public IActionResult PaymentExecute(IQueryCollection collection)
    {
        try
        {
            var response = _momoService.PaymentExecuteAsync(collection);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Có lỗi xảy ra: " + ex.Message });
        }
        }
    [HttpPost("vnpay/payment")]
    public async Task<IActionResult> CreatePaymentUrlVnpay(int TournamentID)
    {

        var url = _vnPayService.CreatePaymentUrl(TournamentID, HttpContext);

        return Ok(new { PayUrl = url });
    }

    [HttpGet("vnpay/PaymentExecute")]

    public async Task<IActionResult> PaymentCallbackVnpay()
    {
        var vnpOrderInfo = Request.Query["vnp_OrderInfo"].ToString(); // Lấy OrderInfo
        var tournamentId = ExtractTournamentIdFromOrderInfo(vnpOrderInfo);

        var response = _vnPayService.PaymentExecute(Request.Query);

        var inputInvoice = new CreateInvoiceDto
        {
            InvoiceNumber = response.OrderId,
            PaymentID = response.PaymentId,
            TransactionID = response.TransactionId,
            PaymentMethod = response.PaymentMethod,
            CreatedDate = DateTime.UtcNow,
            Status = true,
        };

        if (response.VnPayResponseCode == "00")
        {
            await _emailService.SendInvoiceEmail(tournamentId);
            await _invoiceService.CreateInvoice(inputInvoice);
            await _orderService.ConfirmOrder(response.PaymentId , tournamentId , response.PaymentMethod);

            return Json(new { response, tournamentId });
        }

        return BadRequest(new { message = "Payment failed", code = response.VnPayResponseCode });
    }

    // Hàm hỗ trợ tách TournamentID
    private int ExtractTournamentIdFromOrderInfo(string orderInfo)
    {
        // Giả định orderInfo có format: "Thanh toán hóa đơn cho giải đấu {TournamentID}"
        var words = orderInfo.Split(' ');
        var tournamentIdString = words.Last(); // Lấy phần cuối cùng chứa TournamentID

        if (int.TryParse(tournamentIdString, out int tournamentId))
        {
            return tournamentId; // Thành công
        }

        throw new FormatException("Invalid Tournament ID in OrderInfo");
    }





}