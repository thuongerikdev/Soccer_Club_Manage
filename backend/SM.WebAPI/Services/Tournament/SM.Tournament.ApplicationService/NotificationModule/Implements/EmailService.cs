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
            var subject = "Tournament Activation Confirmation";

            // Creating a professional HTML email body
           

            var order = await _orderService.getOrderByTour(TournamentID);
            var orderRes = order.Data as Orders;
            var userID = orderRes.UserID;

            var user = await _userInforSerivce.GetUserInforAsync(userID);
            var userRespone = user as getUserDto;

            var email = userRespone?.email;

            if (user == null || string.IsNullOrWhiteSpace(email))
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "User not found",
                    Data = null
                };
            }
            var body = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                padding: 20px;
                margin: 0;
            }}
            .container {{
                background-color: #ffffff;
                padding: 30px;
                border-radius: 8px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                max-width: 600px;
                margin: auto;
            }}
            .header {{
                text-align: center;
                padding: 10px 0;
                border-bottom: 2px solid #28a745;
            }}
            .footer {{
                text-align: center;
                font-size: 12px;
                color: #888888;
                margin-top: 20px;
                border-top: 1px solid #e0e0e0;
                padding-top: 10px;
            }}
            h1 {{
                color: #28a745;
            }}
            h2 {{
                color: #333333;
            }}
            p {{
                line-height: 1.6;
                color: #555555;
            }}
            .button {{
                background-color: #28a745;
                color: white;
                padding: 12px 25px;
                text-decoration: none;
                border-radius: 5px;
                display: inline-block;
                margin-top: 20px;
                font-weight: bold;
            }}
            .details {{
                background-color: #f9f9f9;
                padding: 15px;
                border-left: 4px solid #28a745;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h1>Your Tournament Has Been Activated!</h1>
            </div>
            <p>Dear {userRespone.name},</p>
            <p>We are excited to inform you that your tournament has been successfully activated. Here are the details:</p>

            <div class='details'>
                <h2>Tournament Details</h2>
                <p><strong>Tournament ID:</strong> {TournamentID}</p>
                <p><strong>Activation Date:</strong> {DateTime.Now.ToString("MMMM dd, yyyy")}</p>
                <p><strong>Amount:</strong> {orderRes.OrderAmount} VNĐ</p>
            </div>

            <p>If you have any questions or need assistance, please do not hesitate to contact us.</p>
            <a href='https://your-tournament-website.com/details/{TournamentID}' class='button'>View Tournament Details</a>
            <p>Thank you for being a part of our community!</p>
            <div class='footer'>
                <p>&copy; {DateTime.Now.Year} Your Company Name. All rights reserved.</p>
                <p><a href='https://your-tournament-website.com/privacy' style='color: #888888;'>Privacy Policy</a> | <a href='https://your-tournament-website.com/terms' style='color: #888888;'>Terms of Service</a></p>
            </div>
        </div>
    </body>
    </html>";

            await SendEmailAsync(email, subject, body);

            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Email sent successfully",
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
