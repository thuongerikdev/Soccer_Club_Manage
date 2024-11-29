using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Abtracts
{
    public interface IPaymentService
    {
        public Task<string> CreatePaymentAsync(int TournamentID);
    }
}
