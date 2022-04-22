using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.SolutionService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddSolutionService(this IServiceCollection services)
        {
            services.AddSingleton<ISolutionService, SolutionService>();

            return services;
        }
    }
}
