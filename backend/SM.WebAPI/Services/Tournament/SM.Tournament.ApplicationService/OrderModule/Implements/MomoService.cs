using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SM.Tournament.Dtos.OrderDto.OrderModel.Momo;
using Microsoft.Extensions.Logging;
using SM.Tournament.Infrastructure;
using SM.Tournament.ApplicationService.Common;

namespace SM.Tournament.ApplicationService.OrderModule.Implements
{
    public class MomoService : TournamentServiceBase , IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;

        public MomoService(IOptions<MomoOptionModel> options , ILogger<MomoService> logger , TournamentDbContext dbContext ) : base(logger, dbContext)
        {
            _options = options;
        }

        public async Task<CreatePaymentResponseModel> CreatePaymentAsync(int TournamentID)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.TournamentID == TournamentID);
            if (order == null)
            {
                return null;
            }
            if (order.PaymentStatus == "Confirmed" && order.OrderStatus == "Confirmed")
            {
                return new CreatePaymentResponseModel()
                {
                    Message = "Đơn hàng đã được thanh toán"
                };
            }
            var orderID = DateTime.UtcNow.Ticks.ToString();
            int amount = (int)order.OrderAmount;
            var infor = "thanh toán hóa đơn cho giải đấu " + " với số tiền " + amount + " VNĐ" + " cho giải đấu " + TournamentID;
            var orderType = "other";

            //model.OrderId = DateTime.UtcNow.Ticks.ToString();
            //model.OrderInfo = "Khách hàng: " + model.FullName + ". Nội dung: " + model.OrderInfo;
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}" +
                $"&accessKey={_options.Value.AccessKey}" +
                $"&requestId={orderID}" +
                $"&amount={amount}" +
                $"&orderId={orderID}" +
                $"&orderInfo={infor}" +
                $"&returnUrl={_options.Value.ReturnUrl}" +
                $"&notifyUrl={_options.Value.NotifyUrl}" +
                $"&extraData=";

            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = orderID,
                amount = amount.ToString(),
                orderInfo = infor,
                requestId = orderID,
                extraData = "",
                signature = signature
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            var momoResponse = JsonConvert.DeserializeObject<CreatePaymentResponseModel>(response.Content);
            return momoResponse;

        }


        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
        {
            var amount = collection.First(s => s.Key == "amount").Value;
            var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
            var orderId = collection.First(s => s.Key == "orderId").Value;
            return new MomoExecuteResponseModel()
            {
                Amount = amount,
                OrderId = orderId,
                OrderInfo = orderInfo
            };
        }


        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }
    }
}