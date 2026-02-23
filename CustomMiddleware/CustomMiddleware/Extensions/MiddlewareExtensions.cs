using CustomMiddleware.Middlewares;
using CustomMiddleware.Options;

namespace CustomMiddleware.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddApiMonitoring(this IServiceCollection services, Action<ApiMonitoringOptions> configureOption) {
            var options = new ApiMonitoringOptions();
            configureOption?.Invoke(options);


            //eğer configureOption verilmiş ise kullan fakat null ise boş bir obje oluştur:
            services.Configure(configureOption ?? (x => { }));

            return services;
        }

        public static IApplicationBuilder UseApiMonitoring(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiMonitoringMiddleware>();
        }
    }
}
