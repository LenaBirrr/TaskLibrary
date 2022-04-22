using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace TaskLibrary.Settings
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddSettings(this IServiceCollection services)
        {
            services.AddSingleton<ISettingsSource, SettingsSource>();
            services.AddSingleton<IApiSettings, ApiSettings>();
            //services.AddSingleton<IIS4Settings, IS4Settings>();
            services.AddSingleton<IIdentityServerSettings, IdentityServerSettings>();
            services.AddSingleton<IGeneralSettings, GeneralSettings>();
            services.AddSingleton<IDbSettings, DbSettings>();

            return services;
        }
    }
}
