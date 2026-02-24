using OptionsPattern;
using OptionsPattern.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHostedService<EmailMonitoringService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//1. eðer tek tek deðer okumak gerekirse:
//var smtp = builder.Configuration.GetSection("EmailSettings").GetValue<string>("SmtpServer");

//2. Bind tekniði:
EmailSettings emailSettings = new EmailSettings();
builder.Configuration.GetSection("EmailSettings").Bind(emailSettings);
//3. Doðrudan get ile alma:

builder.Configuration.Get<EmailSettings>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddSingleton<MailService>();

builder.Services.AddScoped<DynamicMailService>();

var app = builder.Build();

//app.Logger.LogInformation($"Okunan smtp deðeri:{smtp}");
app.Logger.LogInformation($"smtp bilgileri: {emailSettings.SmtpServer}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
