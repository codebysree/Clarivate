using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Clarivate.Services
{
    public class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var healthCheckResult = HealthCheckResult.Healthy("Application is running smoothly.");
            return Task.FromResult(healthCheckResult);
        }
    }
}
