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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
