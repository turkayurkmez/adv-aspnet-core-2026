using POCDependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*
 * 1. Singleton: Birim zamanda sadece 1 adet olur.
 * 2. Scoped: Her istekte yeniden üretilen, o istekteki tüm ihtiyaçlarda kullanýlýr.
 * 3. Transient: Her durumda yeniden üretilir ve kullanýldýktan sonra kaldýrýlýr.
 */

builder.Services.AddSingleton<ISingleton, Singleton>();
builder.Services.AddScoped<IScoped, Scoped>();
builder.Services.AddTransient<ITransient, Transient>();
builder.Services.AddTransient<GuidService>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
