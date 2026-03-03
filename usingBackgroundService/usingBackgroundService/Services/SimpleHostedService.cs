
namespace usingBackgroundService.Services
{
    public class SimpleHostedService(ILogger<SimpleHostedService> logger) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Simple Hosted Service başladı.");
            //uygulama ayağa kalkarken yapılacak işlemler
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Simple Hosted Service durduruluyor.");
            //Graceful shutdown işlemleri burada yapılabilir
            //Ne demek graceful shutdown: uygulama kapatılırken, çalışan işlemlerin düzgün bir şekilde tamamlanmasını sağlamak için yapılan işlemler. Örneğin, açık olan dosyaların kapatılması, veritabanı bağlantılarının sonlandırılması gibi işlemler yapılabilir.
            return Task.CompletedTask;

        }
    }
}
