using Microsoft.Extensions.Options;
using System.Runtime;

namespace OptionsPattern.Services
{
    public class DynamicMailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<DynamicMailService> _logger;

        public DynamicMailService(IOptionsSnapshot<EmailSettings> emailSettings, ILogger<DynamicMailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public void Send()
        {
            _logger.LogInformation($"From: {_emailSettings.UserName} smtp address: {_emailSettings.SmtpServer}");

        }


    }
}
