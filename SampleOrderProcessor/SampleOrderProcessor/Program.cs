using SampleOrderProcessor;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Sample Order Processor";
});

var host = builder.Build();
host.Run();
