using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.BLL.Services;
using SmartBatteryTesterWebApp.DAL.EF;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;
using SmartBatteryTesterWebApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IMeasurementRepository, EFMeasurementRepository>();
builder.Services.AddSingleton<IMeasurementSetRepository, EFMeasurementSetRepository>();
builder.Services.AddSingleton<IMeasurementInputService, MeasurementInputService>();
builder.Services.AddSingleton<ApplicationContext>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
