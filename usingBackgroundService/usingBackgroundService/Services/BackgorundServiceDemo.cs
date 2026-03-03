
namespace usingBackgroundService.Services
{
    public abstract class BackgorundServiceDemo : IHostedService
    {
        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _ = ExecuteAsync(cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                //cancellationToken ile iptal işlemi yapılabilir

            }
            return Task.CompletedTask;
        }
    }
}
