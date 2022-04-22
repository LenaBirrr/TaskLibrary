using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace TaskLibrary.Api.Configuration.HealthChecks
{
    public class SimpleHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var assembly = Assembly.Load("DSRNetSchool.API");
            var versionNumber = assembly.GetName().Version;

            return HealthCheckResult.Healthy(description: $"Build {versionNumber}");
        }
    }
}
