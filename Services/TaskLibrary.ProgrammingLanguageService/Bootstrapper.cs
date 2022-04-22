using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.ProgrammingLanguageService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddProgrammingLanguageService(this IServiceCollection services)
        {
            services.AddSingleton<IProgrammingLanguageService, ProgrammingLanguageService>();

            return services;
        }
    }
}
