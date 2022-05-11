using TaskLibrary.Db.Context.Context;
using TaskLibrary.Db.Context.Factories;
using TaskLibrary.Settings;

namespace TaskLibrary.Worker.Configuration
{
    public static class DbContextConfiguration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IWorkerSettings settings)
        {
            var dbOptionsDelegate = DbContextOptionFactory.Configure(settings.Db.ConnectionString);

            services.AddDbContextFactory<MainDbContext>(dbOptionsDelegate, ServiceLifetime.Singleton);

            return services;
        }

        public static WebApplication UseAppDbContext(this WebApplication app)
        {
            return app;
        }
    }
}
