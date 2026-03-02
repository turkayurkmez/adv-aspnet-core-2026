using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace usingHealthCheck.Healthchecks
{
    public class RandomHealthCheck : IHealthCheck
    {

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
           var value = Random.Shared.Next(0, 101);
            return value switch
            {
                < 50 => Task.FromResult(HealthCheckResult.Healthy($"Değer normal: {value}")),
                < 80 => Task.FromResult(HealthCheckResult.Degraded($"Değer yüksek: {value}")),
                _ => Task.FromResult(HealthCheckResult.Unhealthy($"Değer kritik: {value}"))
            };

        }
    }
}
