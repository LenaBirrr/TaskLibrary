using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.RabbitMQService.Models;

namespace TaskLibrary.RabbitMQService
{
    public interface IRabbitMqTask
    {
        Task SendEmail(EmailModel email);
    }
}
