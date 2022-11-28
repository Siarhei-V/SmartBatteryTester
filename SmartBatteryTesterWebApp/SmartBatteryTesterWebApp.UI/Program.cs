using Microsoft.EntityFrameworkCore;
using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.BLL.Services;
using SmartBatteryTesterWebApp.DAL.EF;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;
using SmartBatteryTesterWebApp.DAL.Repositories;
using SmartBatteryTesterWebApp.UI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

builder.Services.AddScoped<IMeasurementRepository, EFMeasurementRepository>();
builder.Services.AddScoped<IMeasurementSetRepository, EFMeasurementSetRepository>();
builder.Services.AddScoped<IMeasurementOutputService, MeasurementOutputService>();
builder.Services.AddScoped<ApplicationContext>();
builder.Services.AddTransient<IMeasurementChartDataCreator, MeasurementChartDataCreator>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/About/Error");
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
