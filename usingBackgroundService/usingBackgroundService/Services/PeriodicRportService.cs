
namespace usingBackgroundService.Services
{
    public class PeriodicRportService(ILogger<PeriodicRportService> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                logger.LogInformation("Raporlama işlemi başladı: {Time}", DateTime.UtcNow);
                await DoReportProcessAsync(stoppingToken);
            }
        }

        private async Task DoReportProcessAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            logger.LogInformation("Raporlama işlemi tamamlandı: {Time}", DateTime.UtcNow);
        }
    }
}
