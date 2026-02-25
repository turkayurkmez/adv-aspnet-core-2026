var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddMemoryCache(option =>
//{
//    option.SizeLimit = 1024;
//    option.ExpirationScanFrequency = TimeSpan.FromSeconds(30);

//});

builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching(option =>
{
    
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(60)));

    options.AddPolicy("Aggressive", builder => builder.Expire(TimeSpan.FromHours(1))
                                                      .SetVaryByQuery("id")//id isimli qs deðiþirse      
                                                      .SetVaryByHeader("Accept-Language")// header bilgisine göre (istemciden gelen) 
                                                      .SetVaryByRouteValue("userId") // route parametresine göre 
              );
    options.AddPolicy("Short", builder => builder.Expire(TimeSpan.FromSeconds(30)));


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseOutputCache();

app.UseAuthorization();
app.UseResponseCaching();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
