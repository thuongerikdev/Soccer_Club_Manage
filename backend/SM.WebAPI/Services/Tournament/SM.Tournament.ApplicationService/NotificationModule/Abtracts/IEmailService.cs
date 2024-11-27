using SM.Tournament.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.NotificationModule.Abtracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        public Task TestSend(string to);
        public Task<TournamentResponeDto> SendInvoiceEmail(int TournamentID);
    }
}
