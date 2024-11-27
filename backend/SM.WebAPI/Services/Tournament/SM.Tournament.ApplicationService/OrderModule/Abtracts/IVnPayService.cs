using Microsoft.AspNetCore.Http;
using SM.Tournament.Dtos.OrderDto.OrderModel.VnPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Abtracts
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(int TournamentID, HttpContext context );
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
