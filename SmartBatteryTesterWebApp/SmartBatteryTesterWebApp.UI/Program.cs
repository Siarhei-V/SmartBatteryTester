using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.BLL.Services;
using SmartBatteryTesterWebApp.DAL.EF;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;
using SmartBatteryTesterWebApp.DAL.Repositories;
using SmartBatteryTesterWebApp.UI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMeasurementRepository, EFMeasurementRepository>();
builder.Services.AddSingleton<IMeasurementSetRepository, EFMeasurementSetRepository>();
builder.Services.AddSingleton<IMeasurementOutputService, MeasurementOutputService>();
builder.Services.AddSingleton<ApplicationContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=About}/{action=Index}/{id?}");

app.MapHub<MeasurementsHub>("/measurementsHub");

app.Run();
