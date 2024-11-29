using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Dtos.OrderDto.OrderModel.VnPay;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Implements
{
    public class VnPayService : TournamentServiceBase ,IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IInvoiceService _invoiceService;

        public VnPayService(IConfiguration configuration , ILogger <VnPayService> logger , TournamentDbContext dbContext ,IInvoiceService invoiceService ) : base(logger, dbContext)
        {
            _configuration = configuration;
            _invoiceService = invoiceService;
        }

        public string CreatePaymentUrl(int TournamentID, HttpContext context )
        {


            var order = _dbContext.Orders.FirstOrDefault (x => x.TournamentID == TournamentID);
            if (order == null)
            {
                return null;
            }
            if (order.PaymentStatus == "Confirmed" && order.OrderStatus == "Confirmed")
            {
                return "Đơn hàng đã được thanh toán";
            }
            var orderID = order.OrderID;
            var amount = order.OrderAmount;
            var infor = "thanh toán hóa đơn cho giải đấu " + " với số tiền " + amount + " VNĐ" + " cho giải đấu " +TournamentID;
            var orderType = "other";

            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentBackReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", infor);
            pay.AddRequestData("vnp_OrderType", orderType);
            pay.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:PaymentBackReturnUrl"]);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }


        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            return response;
        }
        

    }
}
