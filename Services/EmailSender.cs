using Dimension_Data.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Dimension_Data.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOpt> options)
        {
            Options = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string msg)
        {
            return Execute(Options.SendGridKey, subject, msg, email);
        }

        private Task Execute(string key, string subject, string msg, string email)
        {
            var client = new SendGridClient(key);
            var message = new SendGridMessage()
            {
                From = new EmailAddress("30089662@g.nwu.ac.za", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = msg,
                HtmlContent = msg
            };
            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);
            return client.SendEmailAsync(message);
        }

        public AuthMessageSenderOpt Options { get; }
    }
    
}
