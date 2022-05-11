using TaskLibrary.RabbitMQService;
using TaskLibrary.Settings;
using TaskLibrary.SMTPService;

namespace TaskLibrary.Worker
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddSettings()
                .AddEmailSender()
                .AddRabbitMq()
                .AddSingleton<ITaskExecutor, TaskExecutor>();
            ;

            return services;
        }
    }
}
