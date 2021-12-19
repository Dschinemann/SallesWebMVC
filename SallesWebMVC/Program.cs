using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SallesWebMVC.Data;
using SallesWebMVC.Services;
using System.Globalization;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SallesWebMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SallesWebMVCContext")));

builder.Services.AddScoped<SeedingService, SeedingService>();
builder.Services.AddScoped<SellersService>();
builder.Services.AddScoped<DepartmentServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    var enUS = new CultureInfo("en-US");
    var localizationOptions = new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(enUS),
        SupportedCultures = new List<CultureInfo> { enUS },
        SupportedUICultures = new List<CultureInfo> { enUS }
    };
    app.UseRequestLocalization(localizationOptions);
}
   



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
