
using Microsoft.Extensions.Options;

namespace OptionsPattern.Services
{
    public class EmailMonitoringService : IHostedService
    {

        private readonly ILogger<EmailMonitoringService> _logger;
        private readonly IOptionsMonitor<EmailSettings> _optionsMonitor;

        private IDisposable _mailListener;

        public EmailMonitoringService(ILogger<EmailMonitoringService> logger, IOptionsMonitor<EmailSettings> optionsMonitor)
        {
            _logger = logger;
            _optionsMonitor = optionsMonitor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _optionsMonitor.OnChange(settings => _logger.LogInformation($"Email ayarları değişti. Smtpserver: {settings.SmtpServer}, kullanıcı: {settings.UserName}"));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _mailListener.Dispose();
            return Task.CompletedTask;
        }
    }
}
