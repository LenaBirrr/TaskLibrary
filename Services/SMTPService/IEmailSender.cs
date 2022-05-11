using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.RabbitMQService.Models;

namespace TaskLibrary.SMTPService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel model);
        Task SendEmailAsync(string email, string subject, string message);
    }
}
