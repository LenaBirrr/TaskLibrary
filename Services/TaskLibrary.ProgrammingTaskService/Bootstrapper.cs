using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.ProgrammingTaskService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddProgrammingTaskService(this IServiceCollection services)
        {
            services.AddSingleton<IProgrammingTaskService, ProgrammingTaskService>();

            return services;
        }
    }
}
