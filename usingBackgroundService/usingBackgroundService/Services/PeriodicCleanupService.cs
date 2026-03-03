
namespace usingBackgroundService.Services
{
    public class PeriodicCleanupService(ILogger<PeriodicCleanupService> logger) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Temizlik işlemi başladı: {Time}", DateTime.UtcNow);
                await DoCleanProcessAsync(stoppingToken);
            }
        }

        private async Task DoCleanProcessAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            logger.LogInformation("Temizlik işlemi tamamlandı: {Time}", DateTime.UtcNow);
        }
    }
}
