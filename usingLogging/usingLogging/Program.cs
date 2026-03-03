using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddEventLog();
builder.Logging.AddDebug();

//Dikkat: tüm konfigürasyonlarý appsettings.json dosyasýndan okuyarak yapacaðýz. Bu nedenle aþaðýdaki kodu ekliyoruz.
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


//eðer appsettings.json dosyasýndaki Serilog konfigürasyonunu okumazsak, aþaðýdaki gibi manuel olarak konfigüre edebiliriz.
Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            //.MinimumLevel.Information()
            //.Enrich.FromLogContext()
            //.Enrich.WithMachineName()
            //.Enrich.WithEnvironmentName()
            //.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")

            //.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

// Serilog'u ASP.NET Core uygulamasýna entegre edin
builder.Host.UseSerilog();




// Add services to the container.
Log.Information("Uygulama baþlatýlýyor...");

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
