using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.Interfaces.Emails;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Harmoniq.BLL.Services.Emails
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _fromPassword;

        public EmailSender(IConfiguration configuration)
        {
            var smtpSettings = configuration.GetSection("SmtpSettings");
            _smtpServer = smtpSettings.GetValue<string>("Server");
            _smtpPort = smtpSettings.GetValue<int>("Port");
            _fromEmail = smtpSettings.GetValue<string>("FromEmail");
            _fromPassword = smtpSettings.GetValue<string>("FromPassword");
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(_fromEmail, _fromPassword);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}