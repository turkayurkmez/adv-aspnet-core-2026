using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        //partition, IP Adresi, kullanýcý kimliði, "fixed" olabilir.
        return RateLimitPartition.GetFixedWindowLimiter<string>(partitionKey:"fixed", factory: partition =>
            new FixedWindowRateLimiterOptions
            {
                PermitLimit = 20,
                Window = TimeSpan.FromSeconds(60),//60 saniyede 20 istek
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,//kuyrukta bekleyen istekler eski tarihten yeni tarihe doðru iþlenir
                QueueLimit = 0, // kuyrukta bekleyen istek olmaz, hemen reddedilir 
                AutoReplenishment = true // izinler otomatik olarak yenilenir

            });
    });
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", cancellationToken);
    };

    //sliding window rate limiter ekleyelim, sliding window rate limiter, belirli bir zaman diliminde belirli sayýda isteðe izin verir ve bu zaman dilimi kayar. Örneðin, 30 saniyede 10 istek izni verirse ve windows 6 segmente bölünürse, her segment 5 saniye olur. Bu durumda, her 5 saniyede bir segment yenilenir ve toplamda 30 saniyede 10 istek izni verilir. Bu sayede, belirli bir zaman diliminde belirli sayýda isteðe izin verilirken, bu zaman dilimi kayar ve daha esnek bir rate limiting saðlar.

    options.AddSlidingWindowLimiter("sliding", factory =>
        new SlidingWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(30),//30 saniyede 10 istek
            SegmentsPerWindow = 6, // pencere 6 segmente bölünür, her segment 5 saniye olur
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0,
            AutoReplenishment = true
        });


    options.AddTokenBucketLimiter("tokenbucket", factory =>
        new TokenBucketRateLimiterOptions
        {
            TokenLimit = 5, // bucket'ta maksimum 5 jeton olabilir
            TokensPerPeriod = 5, // her 30 saniyede bucket'a 5 jeton eklenir
            ReplenishmentPeriod = TimeSpan.FromSeconds(30),
            AutoReplenishment = true,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });

    options.AddConcurrencyLimiter("concurrency", factory =>
        new ConcurrencyLimiterOptions
        {
            PermitLimit = 2, // ayný anda en fazla 2 istek iþlenebilir
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0 // kuyrukta bekleyen istek olmaz, hemen reddedilir
        });

    


});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

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


app.MapGet("/sliding", () => $"This endpoint is protected by a sliding window rate limiter. {DateTime.Now}")
.RequireRateLimiting("sliding"); //bu endpoint sliding window rate limiter kullanýr

app.MapGet("/tokenbucket", () => $"This endpoint is protected by a token bucket rate limiter. {DateTime.Now}")
    .RequireRateLimiting("tokenbucket"); //bu endpoint token bucket rate limiter kullanýr

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
