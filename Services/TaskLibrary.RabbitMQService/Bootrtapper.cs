using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.RabbitMQService
{
    public static class Bootrtapper
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMq, RabbitMq>();
            services.AddSingleton<IRabbitMqTask, RabbitMqTask>();

            return services;
        }
    }
}
