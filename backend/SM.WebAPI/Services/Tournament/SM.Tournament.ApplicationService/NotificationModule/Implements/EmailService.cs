using Microsoft.Extensions.Options;
using SM.Tournament.ApplicationService.NotificationModule.Abtracts;
using SM.Tournament.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using SM.Shared.ApplicationService.User;
using MailKit.Security;
using SM.Tournament.Dtos.OrderDto;
using SM.Tournament.Dtos;
using SM.Tournament.ApplicationService.TourModule.Abtracts;
using SM.Auth.Dtos;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Domain.Orders;
using SM.Shared.ApplicationService.Dto;

namespace SM.Tournament.ApplicationService.NotificationModule.Implements
{

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IUserInforSerivce _userInforSerivce;
        private readonly ITournamentService _tournamentService;
        private readonly IOrderService _orderService;

        public EmailService(IOptions<EmailSettings> emailSettings , IUserInforSerivce userInforSerivce , ITournamentService tournamentService , IOrderService orderService)
        {
            _emailSettings = emailSettings.Value;
            _userInforSerivce = userInforSerivce;
            _tournamentService = tournamentService;
            _orderService = orderService;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail));
            email.To.Add(new MailboxAddress(to, to));
            email.Subject = subject;
            var password = _emailSettings.Password.Trim();


            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                // Specify StartTls explicitly
                await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.Mail, password);
                await smtp.SendAsync(email);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        public async Task<TournamentResponeDto> SendInvoiceEmail(int TournamentID)
        {
            var subject = "Activate Email";
            var body = "Your Tournament Has been Activated ";

            var order = await _orderService.getOrderByTour(TournamentID);

            var orderRes = order.Data as Orders;
            var userID = orderRes.UserID;

            var user = await _userInforSerivce.GetUserInforAsync(userID);

            var userRespone = user as getUserDto;

            var email = userRespone.email;

            if (user == null )
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "User not found",
                    Data = null
                };
            }
            var noti = SendEmailAsync(email, subject, body);

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Send email success",
                Data = userRespone
            };
        }

        public async Task TestSend (string to)
        {
            var subJect = "Activate Email";
            var body = "Your Tournament Has been Activated ";
            await SendEmailAsync(to, subJect, body);
        }
    }
}
