using TaskLibrary.Db.Context.Context;
using TaskLibrary.Db.Context.Factories;
using TaskLibrary.Db.Context.Setup;
using TaskLibrary.Settings;

namespace TaskLibrary.Api.Configuration
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IApiSettings settings)
        {
            var dbOptionsDelegate = DbContextOptionFactory.Configure(settings.Db.ConnectionString);

            services.AddDbContextFactory<MainDbContext>(dbOptionsDelegate, ServiceLifetime.Singleton);

            return services;
        }

        public static IApplicationBuilder UseAppDbContext(this IApplicationBuilder app)
        {
            DbInit.Execute(app.ApplicationServices);

            DbSeed.Execute(app.ApplicationServices);

            return app;
        }
    }
}
