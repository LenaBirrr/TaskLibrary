﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.RabbitMQService.Models
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
