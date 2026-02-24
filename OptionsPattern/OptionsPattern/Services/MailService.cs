using Microsoft.Extensions.Options;

namespace OptionsPattern.Services
{

    /*
     * Eğer appsettings dosyasındaki değerler, doğrudan okunup burada kullanılacaksa IOptions interface'i kullanılabilir.
     */
    public class MailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<MailService> _logger;

        public MailService(IOptions<EmailSettings> emailSettingsOption, ILogger<MailService> logger)
        {

           //IOptions<> înterface'i, appsettings'den bind edilmiş tipi tüm uygulama boyunca taşır.

            _settings = emailSettingsOption.Value;
            _logger = logger;
            
        }

        public void Send()
        {
            _logger.LogInformation($"From: {_settings.UserName} smtp address: {_settings.SmtpServer}");
        }
    }
}
