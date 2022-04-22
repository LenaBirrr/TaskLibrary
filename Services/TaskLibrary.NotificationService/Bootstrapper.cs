using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.NotificationService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddNotificationService(this IServiceCollection services)
        {
            services.AddSingleton<INotificationService, NotificationService>();

            return services;
        }
    }
}
