using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.RabbitMQService.Models;
using TaskLibrary.Settings;

namespace TaskLibrary.SMTPService
{
    internal class EmailSender : IEmailSender
    {
        private readonly IEmailSettings settings;

        public EmailSender(IEmailSettings settings)
        {
            this.settings = settings;
        }

        public async Task SendEmailAsync(EmailModel model)
        {
            await new SMTPProvider(settings).SendEmailAsync(model.Email, model.Subject, model.Message);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await new SMTPProvider(settings).SendEmailAsync(email, subject, message);
        }
    }
}
