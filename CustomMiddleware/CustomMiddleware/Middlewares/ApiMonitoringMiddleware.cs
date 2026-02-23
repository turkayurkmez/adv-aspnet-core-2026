using CustomMiddleware.Options;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CustomMiddleware.Middlewares
{
    public class ApiMonitoringMiddleware
    {
        //bir sonraki middleware, .net core tarafından set edilecek:
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiMonitoringMiddleware> _logger;
        private readonly ApiMonitoringOptions _options;

        public ApiMonitoringMiddleware(RequestDelegate next, ILogger<ApiMonitoringMiddleware> logger, IOptions<ApiMonitoringOptions> options)
        {
            _next = next;
            _logger = logger;
            _options = options.Value;


        }

        public async Task InvokeAsync(HttpContext context) {

            /*
             * Bazı metrikleri toplar (süre, durum kodu vs).
             * Yavaş istekleri tespit et
             * İstek/Yanıt içeriğini logla
             * Koşullu kullanım
             */

            var requestTime = DateTime.Now;
            var  stopWatch = Stopwatch.StartNew();
            // TODO 1: Body kontrolü

            var originalResponseBody = context.Response.Body;
            await _next(context);
            stopWatch.Stop();
            var statusCode = context.Response.StatusCode;

            string fastingInfo = stopWatch.ElapsedMilliseconds > _options.SlowRequestTreshold ? "Yavaş" : "Hızlı";

            _logger.LogInformation($"{context.Request.Path} İstek,  {stopWatch.ElapsedMilliseconds} milisaniyede yanıtlandı. Bu yanıt {fastingInfo} Gelen yanıtın kodu: {statusCode}");

        }


    }
}
