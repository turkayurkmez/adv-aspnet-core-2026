using System.Reflection;

namespace mini.API.REPR
{
    public static class EndpointExtensions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            var endpointTypes = assembly.GetExportedTypes()
                                        .Where(x => x.IsClass && !x.IsAbstract && typeof(IEndpoint).IsAssignableFrom(x));


            foreach (var endpointType in endpointTypes)
                services.AddTransient(typeof(IEndpoint), endpointType);

            return services;
        }

        public static WebApplication MapEndpoints(this WebApplication app)
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();


            var productsGroup = app.MapGroup("/api/products").WithTags("Products");
            foreach (var endpoint in endpoints)
                endpoint.MapEndpoint(productsGroup);
            return app;
        }
    }
}
