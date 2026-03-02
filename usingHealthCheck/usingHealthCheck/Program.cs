using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using usingHealthCheck.Healthchecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHealthChecks()
                .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "SQL Server", tags: new[] { "db", "sql" })
                .AddUrlGroup(new Uri("https://swapi.dev/"), name: "Star Wars Api", tags: new[] { "external", "http" })
                .AddProcessAllocatedMemoryHealthCheck(maximumMegabytesAllocated: 512, name: "Memory")
                .AddCheck<RandomHealthCheck>("random-check", tags: new[] { "random" });

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(10); // Health check'lerin deðerlendirme süresini 10 saniye olarak ayarlayýn
    options.AddHealthCheckEndpoint("Production Api", "/health");
}).AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}




app.UseHttpsRedirection();

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui"; // HealthChecks UI'ya eriþim yolu
    options.ApiPath = "/health-ui-api"; // HealthChecks API'ye eriþim yolu
});

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
