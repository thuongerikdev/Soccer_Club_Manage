using Microsoft.AspNetCore.Http;
using SM.Tournament.Dtos.OrderDto.OrderModel.Momo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Abtracts
{
    public interface IMomoService
    {
        public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
        public Task<CreatePaymentResponseModel> CreatePaymentAsync(int TournamentID);
    }
}
